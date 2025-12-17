<?php

namespace App\Controllers\Admin;

use App\Core\Auth;
use App\Core\Database;

class UserManagementController
{
    public function index(): void
    {
        Auth::requireAdmin();
        
        $user = Auth::user();
        $db = Database::getInstance();
        
        $users = $db->query('SELECT id, username, email, role, created_at FROM users ORDER BY created_at DESC')->fetchAll();
        
        require __DIR__ . '/../../Views/admin/users/index.php';
    }

    public function edit($id): void
    {
        Auth::requireAdmin();
        
        $user = Auth::user();
        $db = Database::getInstance();
        
        $editUser = $db->query('SELECT id, username, email, role FROM users WHERE id = ?', [$id])->fetch();
        
        if (!$editUser) {
            header('Location: /admin/users');
            exit;
        }
        
        require __DIR__ . '/../../Views/admin/users/edit.php';
    }

    public function update($id): void
    {
        Auth::requireAdmin();
        
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            header('Location: /admin/users');
            exit;
        }
        
        $db = Database::getInstance();
        $role = $_POST['role'] ?? 'user';
        
        $db->query('UPDATE users SET role = ? WHERE id = ?', [$role, $id]);
        
        $_SESSION['success'] = 'Gebruiker succesvol bijgewerkt';
        header('Location: /admin/users');
        exit;
    }

    public function delete($id): void
    {
        Auth::requireAdmin();
        
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            header('Location: /admin/users');
            exit;
        }
        
        // Prevent deleting yourself
        if ($id == $_SESSION['user_id']) {
            $_SESSION['error'] = 'Je kunt jezelf niet verwijderen';
            header('Location: /admin/users');
            exit;
        }
        
        $db = Database::getInstance();
        $db->query('DELETE FROM users WHERE id = ?', [$id]);
        
        $_SESSION['success'] = 'Gebruiker succesvol verwijderd';
        header('Location: /admin/users');
        exit;
    }
}
