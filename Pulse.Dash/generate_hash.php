<?php
// Generate correct password hash for admin123
$password = 'admin123';
$hash = password_hash($password, PASSWORD_DEFAULT);

echo "Password hash voor 'admin123':\n";
echo $hash . "\n\n";
echo "Kopieer deze hash en update je database:\n";
echo "UPDATE users SET password = '$hash' WHERE username = 'admin';\n";
