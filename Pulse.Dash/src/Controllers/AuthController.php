<?php

namespace App\Controllers;

use App\Core\Auth;

class AuthController
{
    public function showLogin(): void
    {
        if (Auth::check()) {
            header('Location: /');
            exit;
        }
        
        require __DIR__ . '/../Views/auth/login.php';
    }

    public function login(): void
    {
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            header('Location: /login');
            exit;
        }

        $username = $_POST['username'] ?? '';
        $password = $_POST['password'] ?? '';

        if (Auth::login($username, $password)) {
            if (Auth::isAdmin()) {
                header('Location: /admin');
            } else {
                header('Location: /');
            }
            exit;
        }

        $_SESSION['error'] = 'Ongeldige inloggegevens';
        header('Location: /login');
        exit;
    }

    public function showRegister(): void
    {
        if (Auth::check()) {
            header('Location: /');
            exit;
        }
        
        require __DIR__ . '/../Views/auth/register.php';
    }

    public function register(): void
    {
        if ($_SERVER['REQUEST_METHOD'] !== 'POST') {
            header('Location: /register');
            exit;
        }

        $username = $_POST['username'] ?? '';
        $email = $_POST['email'] ?? '';
        $password = $_POST['password'] ?? '';
        $confirmPassword = $_POST['confirm_password'] ?? '';

        // Validation
        if (empty($username) || empty($email) || empty($password)) {
            $_SESSION['error'] = 'Alle velden zijn verplicht';
            header('Location: /register');
            exit;
        }

        if ($password !== $confirmPassword) {
            $_SESSION['error'] = 'Wachtwoorden komen niet overeen';
            header('Location: /register');
            exit;
        }

        if (strlen($password) < 6) {
            $_SESSION['error'] = 'Wachtwoord moet minimaal 6 karakters zijn';
            header('Location: /register');
            exit;
        }

        try {
            $db = \App\Core\Database::getInstance();
            $hashedPassword = password_hash($password, PASSWORD_DEFAULT);
            
            $db->query(
                'INSERT INTO users (username, email, password, role) VALUES (?, ?, ?, ?)',
                [$username, $email, $hashedPassword, 'user']
            );

            $_SESSION['success'] = 'Account succesvol aangemaakt! Je kunt nu inloggen.';
            header('Location: /login');
            exit;
        } catch (\PDOException $e) {
            if ($e->getCode() == 23000) {
                $_SESSION['error'] = 'Gebruikersnaam of email bestaat al';
            } else {
                $_SESSION['error'] = 'Er is een fout opgetreden';
            }
            header('Location: /register');
            exit;
        }
    }

    public function logout(): void
    {
        Auth::logout();
        header('Location: /login');
        exit;
    }
}
