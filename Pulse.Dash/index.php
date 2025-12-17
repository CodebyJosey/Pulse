<?php
session_start();
require_once __DIR__ . '/src/bootstrap.php';

use App\Core\Router;
use App\Core\Database;

// Initialize database connection
Database::getInstance();

// Initialize router
$router = new Router();

// Load routes
require_once __DIR__ . '/src/routes.php';

// Dispatch the request
$router->dispatch();
