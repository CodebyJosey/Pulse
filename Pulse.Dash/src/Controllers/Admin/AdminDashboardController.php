<?php

namespace App\Controllers\Admin;

use App\Core\Auth;
use App\Core\Database;

class AdminDashboardController
{
    public function index(): void
    {
        Auth::requireAdmin();
        
        $user = Auth::user();
        $db = Database::getInstance();
        
        // Get statistics
        $totalUsers = $db->query('SELECT COUNT(*) as count FROM users')->fetch()['count'];
        $totalConfigs = $db->query('SELECT COUNT(*) as count FROM plugin_configs')->fetch()['count'];
        $totalServers = $db->query('SELECT COUNT(*) as count FROM minecraft_servers')->fetch()['count'];
        
        require __DIR__ . '/../../Views/admin/dashboard.php';
    }
}
