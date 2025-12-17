<?php

namespace App\Controllers\Admin;

use App\Core\Auth;
use App\Core\Database;

class PluginConfigController
{
    public function index(): void
    {
        Auth::requireAdmin();
        
        $user = Auth::user();
        $db = Database::getInstance();
        
        $configs = $db->query('SELECT * FROM plugin_configs ORDER BY config_key')->fetchAll();
        
        require __DIR__ . '/../../Views/admin/plugin-config/index.php';
    }

    public function save(): void
    {
        Auth::requireAdmin();
        
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            header('Location: /admin/plugin-config');
            exit;
        }
        
        $db = Database::getInstance();
        
        foreach ($_POST as $key => $value) {
            if (strpos($key, 'config_') === 0) {
                $configId = str_replace('config_', '', $key);
                $db->query(
                    'UPDATE plugin_configs SET config_value = ? WHERE id = ?',
                    [$value, $configId]
                );
            }
        }
        
        $_SESSION['success'] = 'Configuratie succesvol opgeslagen';
        header('Location: /admin/plugin-config');
        exit;
    }
}
