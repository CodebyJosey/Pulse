-- Fix admin password
-- Run this if you already imported the database with the wrong hash

USE pulse_dash;

-- Delete existing admin user
DELETE FROM users WHERE username = 'admin';

-- Insert admin with correct password hash for 'admin123'
-- You need to run generate_hash.php first to get the correct hash
-- Then replace the hash below with the generated one

INSERT INTO users (username, email, password, role) VALUES 
('admin', 'admin@pulse.local', '$2y$10$ldNCLt3dZPzqpShVcaXwCuorhFN09aITBFbGRc6heC1MNMgK8mHau', 'admin');
