<?php

namespace App\Controllers;

use App\Core\Auth;

class DashboardController
{
    public function index(): void
    {
        Auth::requireAuth();
        
        $user = Auth::user();
        require __DIR__ . '/../Views/dashboard/index.php';
    }

    public function profile(): void
    {
        Auth::requireAuth();
        
        $user = Auth::user();
        require __DIR__ . '/../Views/dashboard/profile.php';
    }
}
