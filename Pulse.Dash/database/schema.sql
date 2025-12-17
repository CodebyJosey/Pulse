-- Create database
CREATE DATABASE IF NOT EXISTS pulse_dash CHARACTER SET utf8mb4 COLLATE utf8mb4_unicode_ci;

USE pulse_dash;

-- Users table
CREATE TABLE IF NOT EXISTS users (
    id INT AUTO_INCREMENT PRIMARY KEY,
    username VARCHAR(50) UNIQUE NOT NULL,
    email VARCHAR(100) UNIQUE NOT NULL,
    password VARCHAR(255) NOT NULL,
    role ENUM('admin', 'user') DEFAULT 'user',
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Plugin configurations table
CREATE TABLE IF NOT EXISTS plugin_configs (
    id INT AUTO_INCREMENT PRIMARY KEY,
    config_key VARCHAR(100) UNIQUE NOT NULL,
    config_value TEXT,
    description TEXT,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Minecraft servers table (voor toekomstige gebruik)
CREATE TABLE IF NOT EXISTS minecraft_servers (
    id INT AUTO_INCREMENT PRIMARY KEY,
    server_name VARCHAR(100) NOT NULL,
    server_ip VARCHAR(50) NOT NULL,
    api_key VARCHAR(255) NOT NULL,
    is_active BOOLEAN DEFAULT TRUE,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    updated_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP ON UPDATE CURRENT_TIMESTAMP
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Reports table
CREATE TABLE IF NOT EXISTS reports (
    id INT AUTO_INCREMENT PRIMARY KEY,
    title VARCHAR(255) NOT NULL,
    description TEXT,
    report_type ENUM('overview', 'users', 'servers', 'custom') DEFAULT 'overview',
    content LONGTEXT,
    created_by INT NOT NULL,
    created_at TIMESTAMP DEFAULT CURRENT_TIMESTAMP,
    FOREIGN KEY (created_by) REFERENCES users(id) ON DELETE CASCADE
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;

-- Insert default admin user (password: admin123)
INSERT INTO users (username, email, password, role) VALUES 
('admin', 'admin@pulse.local', '$2y$10$ldNCLt3dZPzqpShVcaXwCuorhFN09aITBFbGRc6heC1MNMgK8mHau', 'admin')
ON DUPLICATE KEY UPDATE password = VALUES(password);

-- Insert some default plugin configurations
INSERT INTO plugin_configs (config_key, config_value, description) VALUES
('server_name', 'Pulse Minecraft Server', 'De naam van de Minecraft server'),
('max_players', '100', 'Maximum aantal spelers'),
('enable_whitelist', 'false', 'Whitelist aan/uit zetten'),
('api_endpoint', 'http://localhost:5000/api', 'API endpoint voor de Minecraft plugin');
