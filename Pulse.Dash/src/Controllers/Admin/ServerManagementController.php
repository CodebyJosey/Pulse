<?php

namespace App\Controllers\Admin;

use App\Core\Auth;
use App\Core\Database;

class ServerManagementController
{
    public function index(): void
    {
        Auth::requireAdmin();
        
        $user = Auth::user();
        $db = Database::getInstance();
        
        $servers = $db->query('SELECT * FROM minecraft_servers ORDER BY created_at DESC')->fetchAll();
        
        require __DIR__ . '/../../Views/admin/servers/index.php';
    }

    public function create(): void
    {
        Auth::requireAdmin();
        
        $user = Auth::user();
        require __DIR__ . '/../../Views/admin/servers/create.php';
    }

    public function store(): void
    {
        Auth::requireAdmin();
        
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            header('Location: /admin/servers');
            exit;
        }
        
        $serverName = $_POST['server_name'] ?? '';
        $serverIp = $_POST['server_ip'] ?? '';
        $apiKey = bin2hex(random_bytes(16));
        
        if (empty($serverName) || empty($serverIp)) {
            $_SESSION['error'] = 'Alle velden zijn verplicht';
            header('Location: /admin/servers/create');
            exit;
        }
        
        $db = Database::getInstance();
        $db->query(
            'INSERT INTO minecraft_servers (server_name, server_ip, api_key, is_active) VALUES (?, ?, ?, ?)',
            [$serverName, $serverIp, $apiKey, true]
        );
        
        $_SESSION['success'] = 'Server succesvol toegevoegd';
        header('Location: /admin/servers');
        exit;
    }

    public function delete($id): void
    {
        Auth::requireAdmin();
        
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            header('Location: /admin/servers');
            exit;
        }
        
        $db = Database::getInstance();
        $db->query('DELETE FROM minecraft_servers WHERE id = ?', [$id]);
        
        $_SESSION['success'] = 'Server succesvol verwijderd';
        header('Location: /admin/servers');
        exit;
    }

    public function edit($id): void
    {
        Auth::requireAdmin();
        
        $db = Database::getInstance();
        $server = $db->query('SELECT * FROM minecraft_servers WHERE id = ?', [$id])->fetch();

        if (!$server) {
            $_SESSION['error'] = 'Server niet gevonden';
            header('Location: /admin/servers');
            exit;
        }

        require __DIR__ . '/../../Views/admin/servers/edit.php';
    }

    public function update($id): void
    {
        Auth::requireAdmin();
        
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            header('Location: /admin/servers');
            exit;
        }

        $server_name = $_POST['server_name'] ?? '';
        $server_ip = $_POST['server_ip'] ?? '';
        $is_active = isset($_POST['is_active']) ? 1 : 0;

        if (empty($server_name) || empty($server_ip)) {
            $_SESSION['error'] = 'Alle velden zijn verplicht';
            header("Location: /admin/servers/$id/edit");
            exit;
        }

        $db = Database::getInstance();
        $db->query(
            'UPDATE minecraft_servers SET server_name = ?, server_ip = ?, is_active = ? WHERE id = ?',
            [$server_name, $server_ip, $is_active, $id]
        );

        $_SESSION['success'] = 'Server bijgewerkt';
        header('Location: /admin/servers');
        exit;
    }

    public function toggle($id): void
    {
        Auth::requireAdmin();
        
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            header('Location: /admin/servers');
            exit;
        }
        
        $db = Database::getInstance();
        $server = $db->query('SELECT is_active FROM minecraft_servers WHERE id = ?', [$id])->fetch();
        
        if ($server) {
            $newStatus = !$server['is_active'];
            $db->query('UPDATE minecraft_servers SET is_active = ? WHERE id = ?', [$newStatus, $id]);
            $_SESSION['success'] = 'Server status bijgewerkt';
        }
        
        header('Location: /admin/servers');
        exit;
    }
}
