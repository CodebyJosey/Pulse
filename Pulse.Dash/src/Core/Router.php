<?php

namespace App\Core;

class Router
{
    private array $routes = [];

    public function get(string $path, callable|array $handler): void
    {
        $this->addRoute('GET', $path, $handler);
    }

    public function post(string $path, callable|array $handler): void
    {
        $this->addRoute('POST', $path, $handler);
    }

    private function addRoute(string $method, string $path, callable|array $handler): void
    {
        $this->routes[] = [
            'method' => $method,
            'path' => $path,
            'handler' => $handler
        ];
    }

    public function dispatch(): void
    {
        $requestMethod = $_SERVER['REQUEST_METHOD'];
        $requestUri = parse_url($_SERVER['REQUEST_URI'], PHP_URL_PATH);
        
        // Remove trailing slash except for root
        if ($requestUri !== '/' && substr($requestUri, -1) === '/') {
            $requestUri = rtrim($requestUri, '/');
        }

        foreach ($this->routes as $route) {
            if ($route['method'] === $requestMethod && $this->matchPath($route['path'], $requestUri, $params)) {
                $this->executeHandler($route['handler'], $params);
                return;
            }
        }

        http_response_code(404);
        echo '404 - Page Not Found';
    }

    private function matchPath(string $routePath, string $requestUri, &$params = []): bool
    {
        $params = [];
        
        // Convert route path to regex pattern
        $pattern = preg_replace('/\{([^}]+)\}/', '(?P<$1>[^/]+)', $routePath);
        $pattern = '#^' . $pattern . '$#';

        if (preg_match($pattern, $requestUri, $matches)) {
            foreach ($matches as $key => $value) {
                if (is_string($key)) {
                    $params[$key] = $value;
                }
            }
            return true;
        }

        return false;
    }

    private function executeHandler(callable|array $handler, array $params): void
    {
        if (is_array($handler)) {
            [$controller, $method] = $handler;
            $controllerInstance = new $controller();
            call_user_func_array([$controllerInstance, $method], $params);
        } else {
            call_user_func_array($handler, $params);
        }
    }
}
