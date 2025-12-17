# INSTALLATIE INSTRUCTIES

## Stap 1: .env bestand aanmaken
```bash
copy .env.example .env
```

Pas de database instellingen aan in `.env`:
```
DB_HOST=localhost
DB_NAME=pulse_dash
DB_USER=root
DB_PASS=jouw_wachtwoord
```

## Stap 2: Database aanmaken en importeren

Open MySQL command line of phpMyAdmin en voer uit:
```bash
mysql -u root -p < database/schema.sql
```

Of via phpMyAdmin:
1. Open phpMyAdmin
2. Klik op "Import"
3. Selecteer `database/schema.sql`
4. Klik op "Go"

## Stap 3: Apache configuratie (XAMPP/WAMP)

### Voor XAMPP:
Plaats de `Pulse.Dash` folder in `C:\xampp\htdocs\`

Bewerk `C:\xampp\apache\conf\extra\httpd-vhosts.conf` en voeg toe:
```apache
<VirtualHost *:80>
    DocumentRoot "C:/xampp/htdocs/Pulse/Pulse.Dash"
    ServerName pulse.local
    
    <Directory "C:/xampp/htdocs/Pulse/Pulse.Dash">
        Options Indexes FollowSymLinks
        AllowOverride All
        Require all granted
    </Directory>
</VirtualHost>
```

Bewerk `C:\Windows\System32\drivers\etc\hosts` (als Administrator):
```
127.0.0.1    pulse.local
```

Herstart Apache.

### Voor WAMP:
Plaats de folder in `C:\wamp64\www\`

## Stap 4: Test de installatie

Ga naar: `http://pulse.local/login` of `http://localhost/Pulse/Pulse.Dash/login`

### Default Login:
- **Username:** admin
- **Password:** admin123

⚠️ **Belangrijk:** Verander dit wachtwoord direct na eerste login!

## Troubleshooting

### "Database connection failed"
- Controleer of MySQL draait
- Controleer `.env` instellingen
- Controleer of database `pulse_dash` bestaat

### "404 Not Found" of routes werken niet
- Controleer of mod_rewrite enabled is in Apache
- Controleer of `.htaccess` bestand aanwezig is
- Controleer AllowOverride in Apache config

### PHP errors
- Minimale PHP versie: 8.0
- Zorg dat PDO MySQL extension enabled is
