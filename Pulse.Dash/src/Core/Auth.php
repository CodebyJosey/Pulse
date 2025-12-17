<?php

namespace App\Core;

class Auth
{
    public static function check(): bool
    {
        return isset($_SESSION['user_id']);
    }

    public static function user(): ?array
    {
        if (!self::check()) {
            return null;
        }

        $db = Database::getInstance();
        $stmt = $db->query(
            'SELECT id, username, email, role, created_at FROM users WHERE id = ?',
            [$_SESSION['user_id']]
        );
        
        return $stmt->fetch() ?: null;
    }

    public static function login(string $username, string $password): bool
    {
        $db = Database::getInstance();
        $stmt = $db->query(
            'SELECT id, password, role FROM users WHERE username = ? OR email = ?',
            [$username, $username]
        );
        
        $user = $stmt->fetch();

        if ($user && password_verify($password, $user['password'])) {
            $_SESSION['user_id'] = $user['id'];
            $_SESSION['role'] = $user['role'];
            return true;
        }

        return false;
    }

    public static function logout(): void
    {
        session_destroy();
        session_start();
    }

    public static function isAdmin(): bool
    {
        return self::check() && $_SESSION['role'] === 'admin';
    }

    public static function requireAuth(): void
    {
        if (!self::check()) {
            header('Location: /login');
            exit;
        }
    }

    public static function requireAdmin(): void
    {
        if (!self::isAdmin()) {
            header('Location: /');
            exit;
        }
    }
}
