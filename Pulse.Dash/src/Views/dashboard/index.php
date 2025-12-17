<!DOCTYPE html>
<html lang="nl" class="dark">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Dashboard - Pulse</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
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
                        <a href="/" class="px-4 py-2 bg-dark-200 text-blue-400 rounded-lg text-sm font-medium">
                            <i class="fas fa-home mr-2"></i>Dashboard
                        </a>
                        <a href="/servers" class="px-4 py-2 text-gray-400 hover:bg-dark-200 hover:text-white rounded-lg text-sm font-medium transition">
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
            <h2 class="text-2xl font-bold text-white">Welkom terug, <?= htmlspecialchars($user['username']) ?>!</h2>
            <p class="text-gray-400 mt-1 text-sm">Hier is een overzicht van je Minecraft infrastructuur</p>
        </div>

        <!-- Stats Grid -->
        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4 mb-6">
            <div class="bg-dark-100 border border-gray-800 rounded-lg p-4">
                <div class="flex items-center justify-between">
                    <div class="flex-1">
                        <p class="text-gray-400 text-xs uppercase mb-1">Servers</p>
                        <p class="text-2xl font-bold text-white">0</p>
                    </div>
                    <div class="w-10 h-10 bg-blue-500/10 rounded-lg flex items-center justify-center flex-shrink-0">
                        <i class="fas fa-server text-blue-500"></i>
                    </div>
                </div>
            </div>

            <div class="bg-dark-100 border border-gray-800 rounded-lg p-4">
                <div class="flex items-center justify-between">
                    <div class="flex-1">
                        <p class="text-gray-400 text-xs uppercase mb-1">Spelers</p>
                        <p class="text-2xl font-bold text-white">0</p>
                    </div>
                    <div class="w-10 h-10 bg-green-500/10 rounded-lg flex items-center justify-center flex-shrink-0">
                        <i class="fas fa-users text-green-500"></i>
                    </div>
                </div>
            </div>

            <div class="bg-dark-100 border border-gray-800 rounded-lg p-4">
                <div class="flex items-center justify-between">
                    <div class="flex-1">
                        <p class="text-gray-400 text-xs uppercase mb-1">CPU</p>
                        <p class="text-2xl font-bold text-white">0%</p>
                    </div>
                    <div class="w-10 h-10 bg-purple-500/10 rounded-lg flex items-center justify-center flex-shrink-0">
                        <i class="fas fa-microchip text-purple-500"></i>
                    </div>
                </div>
            </div>

            <div class="bg-dark-100 border border-gray-800 rounded-lg p-4">
                <div class="flex items-center justify-between">
                    <div class="flex-1">
                        <p class="text-gray-400 text-xs uppercase mb-1">RAM</p>
                        <p class="text-2xl font-bold text-white">0 GB</p>
                    </div>
                    <div class="w-10 h-10 bg-yellow-500/10 rounded-lg flex items-center justify-center flex-shrink-0">
                        <i class="fas fa-memory text-yellow-500"></i>
                    </div>
                </div>
            </div>
        </div>

        <!-- Charts & Data -->
        <div class="grid grid-cols-1 lg:grid-cols-2 gap-6 mb-6">
            <!-- Resource Usage Chart -->
            <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
                <div class="flex items-center justify-between mb-4">
                    <h3 class="text-lg font-bold text-white">Resource Gebruik (24u)</h3>
                    <div class="flex space-x-2">
                        <span class="px-2 py-1 bg-blue-500/10 text-blue-400 text-xs rounded">CPU</span>
                        <span class="px-2 py-1 bg-purple-500/10 text-purple-400 text-xs rounded">RAM</span>
                    </div>
                </div>
                <canvas id="resourceChart" height="200"></canvas>
            </div>

            <!-- Player Activity Chart -->
            <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
                <div class="flex items-center justify-between mb-4">
                    <h3 class="text-lg font-bold text-white">Speler Activiteit (7d)</h3>
                    <span class="px-2 py-1 bg-green-500/10 text-green-400 text-xs rounded">Online Spelers</span>
                </div>
                <canvas id="playerChart" height="200"></canvas>
            </div>
        </div>

        <!-- Server List -->
        <div class="bg-dark-100 border border-gray-800 rounded-lg mb-6">
            <div class="p-6 border-b border-gray-800">
                <div class="flex items-center justify-between">
                    <h3 class="text-lg font-bold text-white">
                        <i class="fas fa-server mr-2 text-blue-500"></i>Mijn Servers
                    </h3>
                    <button class="px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white text-sm rounded-lg transition">
                        <i class="fas fa-plus mr-2"></i>Server Toevoegen
                    </button>
                </div>
            </div>
            <div class="p-6">
                <div class="text-center py-12">
                    <i class="fas fa-server text-gray-600 text-5xl mb-4"></i>
                    <p class="text-gray-400 text-lg mb-2">Geen servers gevonden</p>
                    <p class="text-gray-500 text-sm mb-4">Voeg je eerste Minecraft server toe om te beginnen</p>
                    <button class="px-6 py-3 bg-blue-600 hover:bg-blue-700 text-white rounded-lg font-medium transition">
                        <i class="fas fa-plug mr-2"></i>Server Verbinden
                    </button>
                </div>
            </div>
        </div>

        <!-- Quick Actions & Activity -->
        <div class="grid grid-cols-1 lg:grid-cols-3 gap-6">
            <!-- Quick Actions -->
            <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
                <h3 class="text-lg font-bold text-white mb-4">
                    <i class="fas fa-bolt mr-2 text-yellow-500"></i>Snelle Acties
                </h3>
                <div class="space-y-3">
                    <button class="w-full flex items-center p-3 bg-dark-200 hover:bg-dark-300 rounded-lg transition text-left">
                        <i class="fas fa-plug text-blue-500 mr-3"></i>
                        <div>
                            <p class="font-medium text-white text-sm">Server Verbinden</p>
                            <p class="text-gray-400 text-xs">Voeg nieuwe server toe</p>
                        </div>
                    </button>
                    <button class="w-full flex items-center p-3 bg-dark-200 hover:bg-dark-300 rounded-lg transition text-left">
                        <i class="fas fa-book text-purple-500 mr-3"></i>
                        <div>
                            <p class="font-medium text-white text-sm">Documentatie</p>
                            <p class="text-gray-400 text-xs">Plugin handleiding</p>
                        </div>
                    </button>
                    <button class="w-full flex items-center p-3 bg-dark-200 hover:bg-dark-300 rounded-lg transition text-left">
                        <i class="fas fa-terminal text-green-500 mr-3"></i>
                        <div>
                            <p class="font-medium text-white text-sm">Console</p>
                            <p class="text-gray-400 text-xs">Server console openen</p>
                        </div>
                    </button>
                    <button class="w-full flex items-center p-3 bg-dark-200 hover:bg-dark-300 rounded-lg transition text-left">
                        <i class="fas fa-download text-cyan-500 mr-3"></i>
                        <div>
                            <p class="font-medium text-white text-sm">Backup Maken</p>
                            <p class="text-gray-400 text-xs">Server backup aanmaken</p>
                        </div>
                    </button>
                </div>
            </div>

            <!-- Recent Activity -->
            <div class="lg:col-span-2 bg-dark-100 border border-gray-800 rounded-lg p-6">
                <h3 class="text-lg font-bold text-white mb-4">
                    <i class="fas fa-history mr-2 text-green-500"></i>Recente Activiteit
                </h3>
                <div class="space-y-3">
                    <div class="flex items-start p-3 bg-dark-200 rounded-lg">
                        <i class="fas fa-info-circle text-blue-500 mt-1 mr-3"></i>
                        <div class="flex-1">
                            <p class="text-white text-sm font-medium">Welkom bij Pulse Dashboard</p>
                            <p class="text-gray-400 text-xs mt-1">API integratie wordt voorbereid voor je Minecraft plugin configuratie</p>
                            <p class="text-gray-500 text-xs mt-2">Zojuist</p>
                        </div>
                    </div>
                    <div class="flex items-start p-3 bg-dark-200 rounded-lg">
                        <i class="fas fa-check-circle text-green-500 mt-1 mr-3"></i>
                        <div class="flex-1">
                            <p class="text-white text-sm font-medium">Account aangemaakt</p>
                            <p class="text-gray-400 text-xs mt-1">Je account is succesvol geregistreerd</p>
                            <p class="text-gray-500 text-xs mt-2"><?= date('d-m-Y H:i', strtotime($user['created_at'])) ?></p>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Chart.js Configuration -->
    <script>
        // Resource Usage Chart
        const resourceCtx = document.getElementById('resourceChart');
        new Chart(resourceCtx, {
            type: 'line',
            data: {
                labels: ['00:00', '04:00', '08:00', '12:00', '16:00', '20:00', '24:00'],
                datasets: [{
                    label: 'CPU %',
                    data: [12, 19, 15, 25, 22, 30, 28],
                    borderColor: 'rgb(59, 130, 246)',
                    backgroundColor: 'rgba(59, 130, 246, 0.1)',
                    tension: 0.4
                }, {
                    label: 'RAM %',
                    data: [25, 29, 32, 35, 33, 38, 40],
                    borderColor: 'rgb(168, 85, 247)',
                    backgroundColor: 'rgba(168, 85, 247, 0.1)',
                    tension: 0.4
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        labels: {
                            color: '#9ca3af'
                        }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        max: 100,
                        ticks: {
                            color: '#9ca3af'
                        },
                        grid: {
                            color: '#374151'
                        }
                    },
                    x: {
                        ticks: {
                            color: '#9ca3af'
                        },
                        grid: {
                            color: '#374151'
                        }
                    }
                }
            }
        });

        // Player Activity Chart
        const playerCtx = document.getElementById('playerChart');
        new Chart(playerCtx, {
            type: 'bar',
            data: {
                labels: ['Ma', 'Di', 'Wo', 'Do', 'Vr', 'Za', 'Zo'],
                datasets: [{
                    label: 'Spelers',
                    data: [45, 52, 48, 67, 89, 95, 78],
                    backgroundColor: 'rgba(34, 197, 94, 0.5)',
                    borderColor: 'rgb(34, 197, 94)',
                    borderWidth: 1
                }]
            },
            options: {
                responsive: true,
                maintainAspectRatio: false,
                plugins: {
                    legend: {
                        labels: {
                            color: '#9ca3af'
                        }
                    }
                },
                scales: {
                    y: {
                        beginAtZero: true,
                        ticks: {
                            color: '#9ca3af'
                        },
                        grid: {
                            color: '#374151'
                        }
                    },
                    x: {
                        ticks: {
                            color: '#9ca3af'
                        },
                        grid: {
                            color: '#374151'
                        }
                    }
                }
            }
        });
    </script>
</body>
</html>
