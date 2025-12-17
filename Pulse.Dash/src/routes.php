<?php

use App\Controllers\AuthController;
use App\Controllers\DashboardController;
use App\Controllers\ServerController;
use App\Controllers\Admin\AdminDashboardController;
use App\Controllers\Admin\PluginConfigController;
use App\Controllers\Admin\UserManagementController;
use App\Controllers\Admin\ServerManagementController;
use App\Controllers\Admin\ReportController;

// Auth routes
$router->get('/login', [AuthController::class, 'showLogin']);
$router->post('/login', [AuthController::class, 'login']);
$router->get('/register', [AuthController::class, 'showRegister']);
$router->post('/register', [AuthController::class, 'register']);
$router->get('/logout', [AuthController::class, 'logout']);

// User routes
$router->get('/', [DashboardController::class, 'index']);
$router->get('/profile', [DashboardController::class, 'profile']);
$router->get('/servers', [ServerController::class, 'index']);

// Admin routes
$router->get('/admin', [AdminDashboardController::class, 'index']);
$router->get('/admin/users', [UserManagementController::class, 'index']);
$router->get('/admin/users/{id}', [UserManagementController::class, 'edit']);
$router->post('/admin/users/{id}', [UserManagementController::class, 'update']);
$router->post('/admin/users/{id}/delete', [UserManagementController::class, 'delete']);

$router->get('/admin/servers', [ServerManagementController::class, 'index']);
$router->get('/admin/servers/create', [ServerManagementController::class, 'create']);
$router->post('/admin/servers/create', [ServerManagementController::class, 'store']);
$router->get('/admin/servers/{id}/edit', [ServerManagementController::class, 'edit']);
$router->post('/admin/servers/{id}/edit', [ServerManagementController::class, 'update']);
$router->post('/admin/servers/{id}/delete', [ServerManagementController::class, 'delete']);
$router->post('/admin/servers/{id}/toggle', [ServerManagementController::class, 'toggle']);

$router->get('/admin/plugin-config', [PluginConfigController::class, 'index']);
$router->post('/admin/plugin-config', [PluginConfigController::class, 'save']);

$router->get('/admin/reports', [ReportController::class, 'index']);
$router->get('/admin/reports/create', [ReportController::class, 'create']);
$router->post('/admin/reports/create', [ReportController::class, 'store']);
$router->get('/admin/reports/{id}/pdf', [ReportController::class, 'exportPdf']);
$router->post('/admin/reports/{id}/delete', [ReportController::class, 'delete']);
