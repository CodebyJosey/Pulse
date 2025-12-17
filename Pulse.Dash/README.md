# Pulse Dashboard

PHP Dashboard voor Minecraft Plugin configuratie met Tailwind CSS.

## Setup

### Vereisten
- PHP 8.0 of hoger
- MySQL 5.7 of hoger
- Apache met mod_rewrite

### Installatie

1. Kopieer `.env.example` naar `.env` en pas de database instellingen aan:
```bash
copy .env.example .env
```

2. Maak de database aan en importeer het schema:
```bash
mysql -u root -p < database/schema.sql
```

3. Configureer je webserver om de `Pulse.Dash` map als document root te gebruiken.

### Default Login

**Admin Account:**
- Username: `admin`
- Password: `admin123`

**Let op:** Verander dit wachtwoord na eerste login!

## Structuur

```
Pulse.Dash/
├── config/           # Configuratie bestanden
├── database/         # SQL migratie scripts
├── src/
│   ├── Controllers/  # Controllers (Admin en User)
│   ├── Core/         # Core klassen (Router, Auth, Database)
│   └── Views/        # HTML/PHP views met Tailwind CSS
├── .htaccess         # Apache rewrite rules
└── index.php         # Entry point
```

## Features

- ✅ Admin en User rollen
- ✅ Login/Registratie systeem
- ✅ Admin dashboard op `/admin`
- ✅ User dashboard op `/`
- ✅ Tailwind CSS styling
- ✅ MySQL database
- 🚧 API integratie (komt later)
- 🚧 Minecraft plugin configuratie
