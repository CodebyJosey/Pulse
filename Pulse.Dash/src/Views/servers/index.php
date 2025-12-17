<!DOCTYPE html>
<html lang="nl" class="dark">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Servers - Pulse Dashboard</title>
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
<body class="bg-dark-50 text-gray-100 min-h-screen">
    <!-- Navigation -->
    <nav class="bg-dark-100 border-b border-gray-800 shadow-xl">
        <div class="max-w-7xl mx-auto px-4">
            <div class="flex justify-between items-center py-4">
                <div class="flex items-center space-x-6">
                    <h1 class="text-2xl font-bold bg-gradient-to-r from-blue-400 to-purple-500 bg-clip-text text-transparent">
                        <i class="fas fa-cube mr-2 text-blue-500"></i>Pulse Dashboard
                    </h1>
                    <div class="flex space-x-1">
                        <a href="/" class="px-4 py-2 text-gray-400 hover:bg-dark-200 hover:text-white rounded-lg text-sm font-medium transition">
                            <i class="fas fa-home mr-2"></i>Dashboard
                        </a>
                        <a href="/servers" class="px-4 py-2 bg-dark-200 text-blue-400 rounded-lg text-sm font-medium">
                            <i class="fas fa-server mr-2"></i>Servers
                        </a>
                        <a href="/profile" class="px-4 py-2 text-gray-400 hover:bg-dark-200 hover:text-white rounded-lg text-sm font-medium transition">
                            <i class="fas fa-user mr-2"></i>Profiel
                        </a>
                    </div>
                </div>
                <div class="flex items-center space-x-4">
                    <div class="flex items-center space-x-2 px-3 py-2 bg-dark-200 rounded-lg">
                        <div class="w-8 h-8 rounded-full bg-gradient-to-br from-blue-500 to-purple-600 flex items-center justify-center text-white text-sm font-bold">
                            <?= strtoupper(substr($user['username'], 0, 1)) ?>
                        </div>
                        <span class="text-gray-300 text-sm font-medium">
                            <?= htmlspecialchars($user['username']) ?>
                        </span>
                    </div>
                    <a href="/logout" class="bg-red-600 hover:bg-red-700 text-white px-4 py-2 rounded-lg text-sm font-medium transition">
                        <i class="fas fa-sign-out-alt mr-2"></i>Uitloggen
                    </a>
                </div>
            </div>
        </div>
    </nav>

    <!-- Main Content -->
    <div class="max-w-7xl mx-auto px-4 py-6">
        <div class="mb-6">
            <h2 class="text-2xl font-bold text-white">Mijn Servers</h2>
            <p class="text-gray-400 mt-1 text-sm">Overzicht van alle beschikbare Minecraft servers</p>
        </div>

        <?php if (empty($servers)): ?>
            <div class="bg-dark-100 border border-gray-800 rounded-lg p-12 text-center">
                <i class="fas fa-server text-gray-600 text-6xl mb-4"></i>
                <h3 class="text-xl font-bold text-white mb-2">Geen servers beschikbaar</h3>
                <p class="text-gray-400">Er zijn momenteel geen actieve servers geconfigureerd.</p>
            </div>
        <?php else: ?>
            <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                <?php foreach ($servers as $server): ?>
                    <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
                        <div class="flex items-start justify-between mb-4">
                            <div>
                                <h3 class="text-lg font-bold text-white"><?= htmlspecialchars($server['server_name']) ?></h3>
                                <p class="text-gray-400 text-sm"><?= htmlspecialchars($server['server_ip']) ?></p>
                            </div>
                            <span class="px-3 py-1 bg-green-500/10 text-green-400 text-xs rounded-full">
                                <i class="fas fa-circle text-xs mr-1"></i>Online
                            </span>
                        </div>
                        
                        <div class="space-y-3 mb-4">
                            <div class="flex justify-between items-center">
                                <span class="text-gray-400 text-sm">Spelers</span>
                                <span class="text-white font-semibold">0/100</span>
                            </div>
                            <div class="flex justify-between items-center">
                                <span class="text-gray-400 text-sm">Versie</span>
                                <span class="text-white font-semibold">1.20.4</span>
                            </div>
                            <div class="flex justify-between items-center">
                                <span class="text-gray-400 text-sm">Uptime</span>
                                <span class="text-white font-semibold">N/A</span>
                            </div>
                        </div>

                        <button class="w-full bg-blue-600 hover:bg-blue-700 text-white py-2 rounded-lg font-medium transition">
                            <i class="fas fa-play mr-2"></i>Verbinden
                        </button>
                    </div>
                <?php endforeach; ?>
            </div>
        <?php endif; ?>
    </div>
</body>
</html>
