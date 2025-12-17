<?php 
$pageTitle = 'Admin Dashboard';
$includeChartJs = true;
require_once __DIR__ . '/includes/header.php'; 
require_once __DIR__ . '/includes/sidebar.php'; 
?>

<!-- Main Content -->
<div class="flex-1 flex flex-col overflow-hidden">
    <div class="flex-1 overflow-y-auto">
        <div class="p-6">
            <div class="mb-6">
                <h2 class="text-2xl font-bold text-white">Systeem Overzicht</h2>
                <p class="text-gray-400 mt-1 text-sm">Real-time monitoring en beheer van het volledige platform</p>
            </div>

        <!-- Primary Stats Grid -->
        <div class="grid grid-cols-1 sm:grid-cols-2 lg:grid-cols-4 gap-4 mb-6">
            <div class="bg-dark-100 border border-gray-800 rounded-lg p-4">
                <div class="flex items-center justify-between">
                    <div class="flex-1">
                        <p class="text-gray-400 text-xs uppercase mb-1">Gebruikers</p>
                        <p class="text-2xl font-bold text-white"><?= $totalUsers ?></p>
                    </div>
                    <div class="w-10 h-10 bg-blue-500/10 rounded-lg flex items-center justify-center flex-shrink-0">
                        <i class="fas fa-users text-blue-500"></i>
                    </div>
                </div>
            </div>

            <div class="bg-dark-100 border border-gray-800 rounded-lg p-4">
                <div class="flex items-center justify-between">
                    <div class="flex-1">
                        <p class="text-gray-400 text-xs uppercase mb-1">Servers</p>
                        <p class="text-2xl font-bold text-white"><?= $totalServers ?></p>
                    </div>
                    <div class="w-10 h-10 bg-green-500/10 rounded-lg flex items-center justify-center flex-shrink-0">
                        <i class="fas fa-server text-green-500"></i>
                    </div>
                </div>
            </div>

            <div class="bg-dark-100 border border-gray-800 rounded-lg p-4">
                <div class="flex items-center justify-between">
                    <div class="flex-1">
                        <p class="text-gray-400 text-xs uppercase mb-1">Configs</p>
                        <p class="text-2xl font-bold text-white"><?= $totalConfigs ?></p>
                    </div>
                    <div class="w-10 h-10 bg-purple-500/10 rounded-lg flex items-center justify-center flex-shrink-0">
                        <i class="fas fa-cog text-purple-500"></i>
                    </div>
                </div>
            </div>

            <div class="bg-dark-100 border border-gray-800 rounded-lg p-4">
                <div class="flex items-center justify-between">
                    <div class="flex-1">
                        <p class="text-gray-400 text-xs uppercase mb-1">API Calls</p>
                        <p class="text-2xl font-bold text-white">1.2K</p>
                    </div>
                    <div class="w-10 h-10 bg-yellow-500/10 rounded-lg flex items-center justify-center flex-shrink-0">
                        <i class="fas fa-chart-line text-yellow-500"></i>
                    </div>
                </div>
            </div>
        </div>

        <!-- Charts -->
        <div class="flex flex-col lg:flex-row gap-6 mb-6">
            <!-- System Load Chart -->
            <div class="bg-dark-100 border border-gray-800 rounded-lg p-6 flex-1">
                <div class="flex items-center justify-between mb-4">
                    <h3 class="text-lg font-bold text-white">Systeem Belasting (24u)</h3>
                    <div class="flex space-x-2">
                        <span class="px-2 py-1 bg-red-500/10 text-red-400 text-xs rounded">CPU</span>
                        <span class="px-2 py-1 bg-yellow-500/10 text-yellow-400 text-xs rounded">Memory</span>
                    </div>
                </div>
                <div style="height: 200px;">
                    <canvas id="systemChart"></canvas>
                </div>
            </div>

            <!-- User Growth Chart -->
            <div class="bg-dark-100 border border-gray-800 rounded-lg p-6 flex-1">
                <div class="flex items-center justify-between mb-4">
                    <h3 class="text-lg font-bold text-white">Gebruikersgroei (30d)</h3>
                    <span class="px-2 py-1 bg-blue-500/10 text-blue-400 text-xs rounded">Nieuwe Users</span>
                </div>
                <div style="height: 200px;">
                    <canvas id="userGrowthChart"></canvas>
                </div>
            </div>
        </div>

        <!-- Activity & Management Grid -->
        <div class="grid grid-cols-1 lg:grid-cols-3 gap-6 mb-6">
            <!-- Quick Actions -->
            <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
                <h3 class="text-lg font-bold text-white mb-4 flex items-center">
                    <i class="fas fa-bolt mr-2 text-yellow-500"></i>Admin Acties
                </h3>
                <div class="space-y-3">
                    <a href="/admin/users" class="w-full flex items-center p-3 bg-dark-200 hover:bg-dark-300 rounded-lg transition">
                        <i class="fas fa-user-plus text-blue-500 mr-3"></i>
                        <div>
                            <p class="font-medium text-white text-sm">Gebruikers Beheren</p>
                            <p class="text-gray-400 text-xs"><?= $totalUsers ?> gebruikers</p>
                        </div>
                    </a>
                    <a href="/admin/plugin-config" class="w-full flex items-center p-3 bg-dark-200 hover:bg-dark-300 rounded-lg transition">
                        <i class="fas fa-sliders-h text-green-500 mr-3"></i>
                        <div>
                            <p class="font-medium text-white text-sm">Plugin Instellingen</p>
                            <p class="text-gray-400 text-xs"><?= $totalConfigs ?> configuraties</p>
                        </div>
                    </a>
                    <button class="w-full flex items-center p-3 bg-dark-200 hover:bg-dark-300 rounded-lg transition text-left">
                        <i class="fas fa-database text-purple-500 mr-3"></i>
                        <div>
                            <p class="font-medium text-white text-sm">Database Backup</p>
                            <p class="text-gray-400 text-xs">Maak backup aan</p>
                        </div>
                    </button>
                    <button class="w-full flex items-center p-3 bg-dark-200 hover:bg-dark-300 rounded-lg transition text-left">
                        <i class="fas fa-chart-bar text-cyan-500 mr-3"></i>
                        <div>
                            <p class="font-medium text-white text-sm">Rapporten</p>
                            <p class="text-gray-400 text-xs">Bekijk analytics</p>
                        </div>
                    </button>
                </div>
            </div>

            <!-- System Information -->
            <div class="lg:col-span-2 bg-dark-100 border border-gray-800 rounded-lg p-6">
                <h3 class="text-lg font-bold text-white mb-4 flex items-center">
                    <i class="fas fa-info-circle mr-2 text-blue-500"></i>Systeem Informatie
                </h3>
                <div class="grid grid-cols-2 gap-4">
                    <div class="bg-dark-200 rounded-lg p-4">
                        <p class="text-gray-400 text-xs mb-2">PHP Versie</p>
                        <p class="text-white font-bold text-xl"><?= phpversion() ?></p>
                    </div>
                    <div class="bg-dark-200 rounded-lg p-4">
                        <p class="text-gray-400 text-xs mb-2">Webserver</p>
                        <p class="text-white font-semibold text-sm"><?= explode('/', $_SERVER['SERVER_SOFTWARE'] ?? 'Unknown')[0] ?></p>
                    </div>
                    <div class="bg-dark-200 rounded-lg p-4">
                        <p class="text-gray-400 text-xs mb-2">Database</p>
                        <p class="text-white font-semibold">MySQL</p>
                    </div>
                    <div class="bg-dark-200 rounded-lg p-4">
                        <p class="text-gray-400 text-xs mb-2">Platform</p>
                        <p class="text-white font-semibold">Windows</p>
                    </div>
                    <div class="bg-dark-200 rounded-lg p-4">
                        <p class="text-gray-400 text-xs mb-2">Memory Limit</p>
                        <p class="text-white font-semibold"><?= ini_get('memory_limit') ?></p>
                    </div>
                    <div class="bg-dark-200 rounded-lg p-4">
                        <p class="text-gray-400 text-xs mb-2">Max Upload</p>
                        <p class="text-white font-semibold"><?= ini_get('upload_max_filesize') ?></p>
                    </div>
                </div>
            </div>
        </div>

        <!-- Recent Activity Log -->
        <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
            <h3 class="text-lg font-bold text-white mb-4 flex items-center">
                <i class="fas fa-history mr-2 text-green-500"></i>Recente Admin Activiteit
            </h3>
            <div class="space-y-2">
                <div class="flex items-center justify-between p-3 bg-dark-200 rounded-lg">
                    <div class="flex items-center space-x-3">
                        <i class="fas fa-user-plus text-blue-500"></i>
                        <div>
                            <p class="text-white text-sm font-medium">Nieuwe gebruiker geregistreerd</p>
                            <p class="text-gray-400 text-xs">Gebruiker: <?= htmlspecialchars($user['username']) ?></p>
                        </div>
                    </div>
                    <span class="text-gray-500 text-xs">Vandaag</span>
                </div>
                <div class="flex items-center justify-between p-3 bg-dark-200 rounded-lg">
                    <div class="flex items-center space-x-3">
                        <i class="fas fa-cog text-purple-500"></i>
                        <div>
                            <p class="text-white text-sm font-medium">Plugin configuratie bijgewerkt</p>
                            <p class="text-gray-400 text-xs">API endpoint geconfigureerd</p>
                        </div>
                    </div>
                    <span class="text-gray-500 text-xs">2 uur geleden</span>
                </div>
                <div class="flex items-center justify-between p-3 bg-dark-200 rounded-lg">
                    <div class="flex items-center space-x-3">
                        <i class="fas fa-shield-alt text-green-500"></i>
                        <div>
                            <p class="text-white text-sm font-medium">Systeem gestart</p>
                            <p class="text-gray-400 text-xs">Dashboard online gekomen</p>
                        </div>
                    </div>
                    <span class="text-gray-500 text-xs">Vandaag</span>
                </div>
            </div>
        </div>
    </div>

    <!-- Chart.js Configuration -->
    <script>
        // System Load Chart
        const systemCtx = document.getElementById('systemChart');
        new Chart(systemCtx, {
            type: 'line',
            data: {
                labels: ['00:00', '04:00', '08:00', '12:00', '16:00', '20:00', '24:00'],
                datasets: [{
                    label: 'CPU %',
                    data: [15, 22, 18, 28, 25, 32, 30],
                    borderColor: 'rgb(239, 68, 68)',
                    backgroundColor: 'rgba(239, 68, 68, 0.1)',
                    tension: 0.4
                }, {
                    label: 'Memory %',
                    data: [35, 38, 42, 45, 43, 48, 50],
                    borderColor: 'rgb(234, 179, 8)',
                    backgroundColor: 'rgba(234, 179, 8, 0.1)',
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

        // User Growth Chart
        const userGrowthCtx = document.getElementById('userGrowthChart');
        new Chart(userGrowthCtx, {
            type: 'bar',
            data: {
                labels: ['Week 1', 'Week 2', 'Week 3', 'Week 4'],
                datasets: [{
                    label: 'Nieuwe Gebruikers',
                    data: [5, 8, 12, 15],
                    backgroundColor: 'rgba(59, 130, 246, 0.5)',
                    borderColor: 'rgb(59, 130, 246)',
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
                            color: '#9ca3af',
                            stepSize: 5
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
        </div>
    </div>
</div>

<?php require_once __DIR__ . '/includes/footer.php'; ?>
