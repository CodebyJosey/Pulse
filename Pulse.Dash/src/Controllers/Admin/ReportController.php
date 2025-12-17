<?php

namespace App\Controllers\Admin;

use App\Core\Auth;
use App\Core\Database;

class ReportController
{
    private $db;

    public function __construct()
    {
        Auth::requireAdmin();
        $this->db = Database::getInstance();
    }

    public function index()
    {
        $user = Auth::user();
        
        // Get all saved reports
        $reports = $this->db->query("SELECT * FROM reports ORDER BY created_at DESC")->fetchAll();

        require_once __DIR__ . '/../../Views/admin/reports/index.php';
    }

    public function create()
    {
        $user = Auth::user();
        
        // Get statistics for report
        $stats = $this->getStatistics();

        require_once __DIR__ . '/../../Views/admin/reports/create.php';
    }

    public function store()
    {
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            header('Location: /admin/reports/create');
            exit;
        }

        $title = $_POST['title'] ?? '';
        $description = $_POST['description'] ?? '';
        $report_type = $_POST['report_type'] ?? 'overview';

        if (empty($title)) {
            $_SESSION['error'] = 'Titel is verplicht';
            header('Location: /admin/reports/create');
            exit;
        }

        // Get statistics
        $stats = $this->getStatistics();
        $content = json_encode($stats);

        $this->db->query(
            "INSERT INTO reports (title, description, report_type, content, created_by) VALUES (?, ?, ?, ?, ?)",
            [$title, $description, $report_type, $content, $_SESSION['user_id']]
        );

        $_SESSION['success'] = 'Rapport succesvol opgeslagen';
        header('Location: /admin/reports');
        exit;
    }

    public function exportPdf($id)
    {
        $report = $this->db->query("SELECT * FROM reports WHERE id = ?", [$id])->fetch();

        if (!$report) {
            $_SESSION['error'] = 'Rapport niet gevonden';
            header('Location: /admin/reports');
            exit;
        }

        $content = json_decode($report['content'], true);
        
        // Generate HTML for PDF
        $html = $this->generateReportHTML($report, $content);

        // Set headers for PDF download
        header('Content-Type: text/html');
        
        // Use simple HTML to PDF conversion
        echo $html;
        echo "<script>window.print();</script>";
        exit;
    }

    private function getStatistics()
    {
        // Get user count
        $totalUsers = $this->db->query("SELECT COUNT(*) as count FROM users")->fetch()['count'];

        // Get server count
        $totalServers = $this->db->query("SELECT COUNT(*) as count FROM minecraft_servers")->fetch()['count'];

        // Get config count
        $totalConfigs = $this->db->query("SELECT COUNT(*) as count FROM plugin_configs")->fetch()['count'];

        // Get users by role
        $usersByRole = $this->db->query("SELECT role, COUNT(*) as count FROM users GROUP BY role")->fetchAll(\PDO::FETCH_KEY_PAIR);

        // Get active servers
        $activeServers = $this->db->query("SELECT COUNT(*) as count FROM minecraft_servers WHERE is_active = 1")->fetch()['count'];

        return [
            'total_users' => $totalUsers,
            'total_servers' => $totalServers,
            'total_configs' => $totalConfigs,
            'active_servers' => $activeServers,
            'users_by_role' => $usersByRole,
            'generated_at' => date('Y-m-d H:i:s')
        ];
    }

    private function generateReportHTML($report, $content)
    {
        ob_start();
        ?>
        <!DOCTYPE html>
        <html>
        <head>
            <meta charset="UTF-8">
            <title><?= htmlspecialchars($report['title']) ?></title>
            <style>
                body { font-family: Arial, sans-serif; margin: 40px; }
                h1 { color: #dc2626; border-bottom: 2px solid #dc2626; padding-bottom: 10px; }
                h2 { color: #ea580c; margin-top: 30px; }
                .meta { color: #666; margin-bottom: 30px; }
                .stat { background: #f3f4f6; padding: 15px; margin: 10px 0; border-radius: 5px; }
                .stat strong { display: inline-block; width: 200px; }
                table { width: 100%; border-collapse: collapse; margin: 20px 0; }
                th, td { border: 1px solid #ddd; padding: 12px; text-align: left; }
                th { background-color: #dc2626; color: white; }
            </style>
        </head>
        <body>
            <h1><?= htmlspecialchars($report['title']) ?></h1>
            <div class="meta">
                <p><strong>Type:</strong> <?= ucfirst($report['report_type']) ?></p>
                <p><strong>Beschrijving:</strong> <?= htmlspecialchars($report['description']) ?></p>
                <p><strong>Gegenereerd op:</strong> <?= date('d-m-Y H:i', strtotime($report['created_at'])) ?></p>
            </div>

            <h2>Statistieken Overzicht</h2>
            <div class="stat">
                <strong>Totaal Gebruikers:</strong> <?= $content['total_users'] ?>
            </div>
            <div class="stat">
                <strong>Totaal Servers:</strong> <?= $content['total_servers'] ?>
            </div>
            <div class="stat">
                <strong>Actieve Servers:</strong> <?= $content['active_servers'] ?>
            </div>
            <div class="stat">
                <strong>Totaal Configuraties:</strong> <?= $content['total_configs'] ?>
            </div>

            <h2>Gebruikers per Rol</h2>
            <table>
                <thead>
                    <tr>
                        <th>Rol</th>
                        <th>Aantal</th>
                    </tr>
                </thead>
                <tbody>
                    <?php foreach ($content['users_by_role'] as $role => $count): ?>
                    <tr>
                        <td><?= ucfirst($role) ?></td>
                        <td><?= $count ?></td>
                    </tr>
                    <?php endforeach; ?>
                </tbody>
            </table>

            <p style="margin-top: 50px; color: #666; font-size: 12px;">
                Dit rapport is automatisch gegenereerd door Pulse Dashboard op <?= date('d-m-Y H:i') ?>
            </p>
        </body>
        </html>
        <?php
        return ob_get_clean();
    }

    private function htmlToPdf($html)
    {
        echo $html;
        echo "<script>window.print();</script>";
    }

    public function delete($id)
    {
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            header('Location: /admin/reports');
            exit;
        }

        $this->db->query("DELETE FROM reports WHERE id = ?", [$id]);

        $_SESSION['success'] = 'Rapport verwijderd';
        header('Location: /admin/reports');
        exit;
    }
}
