<?php

namespace App\Controllers;

use App\Core\Auth;
use App\Core\Database;

class ServerController
{
    public function index(): void
    {
        Auth::requireAuth();
        
        $user = Auth::user();
        $db = Database::getInstance();
        
        $servers = $db->query('SELECT * FROM minecraft_servers WHERE is_active = 1 ORDER BY created_at DESC')->fetchAll();
        
        require __DIR__ . '/../Views/servers/index.php';
    }
}
