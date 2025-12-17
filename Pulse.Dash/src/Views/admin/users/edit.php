<!DOCTYPE html>
<html lang="nl" class="dark">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Gebruiker Bewerken - Admin Panel</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <script>
        tailwind.config = {
            darkMode: 'class',
            theme: {
                extend: {
                    colors: {
                        dark: {
                            50: '#0c0a09',
                            100: '#1c1917',
                            200: '#292524',
                            300: '#44403c',
                        }
                    }
                }
            }
        }
    </script>
</head>
<body class="bg-dark-50 text-gray-100">
    <div class="flex h-screen overflow-hidden">
        <!-- Sidebar -->
        <aside class="w-64 bg-dark-100 border-r border-gray-800 flex-shrink-0">
            <div class="p-6 border-b border-gray-800">
                <h1 class="text-xl font-bold flex items-center">
                    <i class="fas fa-shield-alt mr-2 text-red-500"></i>
                    <span class="bg-gradient-to-r from-red-400 to-orange-500 bg-clip-text text-transparent">Admin Panel</span>
                </h1>
            </div>
            <nav class="p-4 space-y-1">
                <a href="/admin" class="flex items-center px-4 py-3 text-gray-400 hover:bg-dark-200 hover:text-white rounded-lg transition">
                    <i class="fas fa-tachometer-alt mr-3 w-5"></i>Dashboard
                </a>
                <a href="/admin/users" class="flex items-center px-4 py-3 bg-dark-200 text-red-400 rounded-lg">
                    <i class="fas fa-users mr-3 w-5"></i>Gebruikers
                </a>
                <a href="/admin/servers" class="flex items-center px-4 py-3 text-gray-400 hover:bg-dark-200 hover:text-white rounded-lg transition">
                    <i class="fas fa-server mr-3 w-5"></i>Servers
                </a>
                <a href="/admin/plugin-config" class="flex items-center px-4 py-3 text-gray-400 hover:bg-dark-200 hover:text-white rounded-lg transition">
                    <i class="fas fa-cog mr-3 w-5"></i>Configuratie
                </a>
                <a href="/admin/reports" class="flex items-center px-4 py-3 text-gray-400 hover:bg-dark-200 hover:text-white rounded-lg transition">
                    <i class="fas fa-file-alt mr-3 w-5"></i>Rapporten
                </a>
            </nav>
            <div class="absolute bottom-0 w-64 p-4 border-t border-gray-800">
                <div class="flex items-center px-4 py-3 bg-dark-200 rounded-lg">
                    <div class="w-8 h-8 rounded-full bg-gradient-to-br from-red-500 to-orange-600 flex items-center justify-center text-white text-xs font-bold mr-3">
                        <?= strtoupper(substr($user['username'], 0, 1)) ?>
                    </div>
                    <div class="flex-1">
                        <p class="text-white text-sm font-medium"><?= htmlspecialchars($user['username']) ?></p>
                        <p class="text-gray-400 text-xs">Administrator</p>
                    </div>
                </div>
                <a href="/logout" class="flex items-center justify-center px-4 py-2 mt-2 bg-red-600 hover:bg-red-700 text-white rounded-lg text-sm font-medium transition">
                    <i class="fas fa-sign-out-alt mr-2"></i>Uitloggen
                </a>
            </div>
        </aside>

        <!-- Main Content -->
        <div class="flex-1 flex flex-col overflow-hidden">
            <div class="flex-1 overflow-y-auto">
                <div class="p-6 max-w-3xl">
        <div class="mb-6">
            <h2 class="text-2xl font-bold text-white">Gebruiker Bewerken</h2>
            <p class="text-gray-400 mt-1 text-sm">Bewerk gebruiker: <?= htmlspecialchars($editUser['username']) ?></p>
        </div>

        <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
            <form method="POST" action="/admin/users/<?= $editUser['id'] ?>" class="space-y-6">
                <div>
                    <label class="block text-sm font-medium text-gray-300 mb-2">
                        Gebruikersnaam
                    </label>
                    <input 
                        type="text" 
                        value="<?= htmlspecialchars($editUser['username']) ?>" 
                        disabled
                        class="w-full px-4 py-2 bg-dark-200 border border-gray-700 rounded-lg text-gray-500"
                    >
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-300 mb-2">
                        Email
                    </label>
                    <input 
                        type="email" 
                        value="<?= htmlspecialchars($editUser['email']) ?>" 
                        disabled
                        class="w-full px-4 py-2 bg-dark-200 border border-gray-700 rounded-lg text-gray-500"
                    >
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-300 mb-2">
                        Rol
                    </label>
                    <select 
                        name="role" 
                        class="w-full px-4 py-2 bg-dark-200 border border-gray-700 rounded-lg text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                    >
                        <option value="user" <?= $editUser['role'] === 'user' ? 'selected' : '' ?>>User</option>
                        <option value="admin" <?= $editUser['role'] === 'admin' ? 'selected' : '' ?>>Admin</option>
                    </select>
                </div>

                <div class="flex space-x-4">
                    <button 
                        type="submit"
                        class="bg-blue-600 hover:bg-blue-700 text-white px-6 py-2 rounded-lg font-medium transition"
                    >
                        <i class="fas fa-save mr-2"></i>Opslaan
                    </button>
                    <a 
                        href="/admin/users"
                        class="bg-gray-600 hover:bg-gray-700 text-white px-6 py-2 rounded-lg font-medium transition inline-block"
                    >
                        <i class="fas fa-times mr-2"></i>Annuleren
                    </a>
                </div>
            </form>
        </div>
                </div>
            </div>
        </div>
    </div>
</body>
</html>
