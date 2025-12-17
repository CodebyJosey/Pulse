<?php 
$pageTitle = 'Server Beheer';
require_once __DIR__ . '/../includes/header.php'; 
require_once __DIR__ . '/../includes/sidebar.php'; 
?>

<!-- Main Content -->
<div class="flex-1 flex flex-col overflow-hidden">
    <div class="flex-1 overflow-y-auto">
        <div class="p-6">
        <div class="mb-6 flex items-center justify-between">
            <div>
                <h2 class="text-2xl font-bold text-white">Server Beheer</h2>
                <p class="text-gray-400 mt-1 text-sm">Beheer alle Minecraft servers</p>
            </div>
            <a href="/admin/servers/create" class="px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded-lg font-medium transition">
                <i class="fas fa-plus mr-2"></i>Server Toevoegen
            </a>
        </div>

        <?php if (isset($_SESSION['success'])): ?>
            <div class="bg-green-900/30 border border-green-500 text-green-400 px-4 py-3 rounded-lg mb-4">
                <?= htmlspecialchars($_SESSION['success']) ?>
                <?php unset($_SESSION['success']); ?>
            </div>
        <?php endif; ?>

        <?php if (isset($_SESSION['error'])): ?>
            <div class="bg-red-900/30 border border-red-500 text-red-400 px-4 py-3 rounded-lg mb-4">
                <?= htmlspecialchars($_SESSION['error']) ?>
                <?php unset($_SESSION['error']); ?>
            </div>
        <?php endif; ?>

        <div class="bg-dark-100 border border-gray-800 rounded-lg overflow-hidden">
            <table class="min-w-full divide-y divide-gray-800">
                <thead class="bg-dark-200">
                    <tr>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">ID</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">Server Naam</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">IP Adres</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">API Key</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">Status</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">Aangemaakt</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">Acties</th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-800">
                    <?php if (empty($servers)): ?>
                        <tr>
                            <td colspan="7" class="px-6 py-12 text-center text-gray-400">
                                Geen servers gevonden. <a href="/admin/servers/create" class="text-blue-400 hover:text-blue-300">Voeg een server toe</a>
                            </td>
                        </tr>
                    <?php else: ?>
                        <?php foreach ($servers as $server): ?>
                            <tr class="hover:bg-dark-200/50">
                                <td class="px-6 py-4 whitespace-nowrap text-sm text-white"><?= $server['id'] ?></td>
                                <td class="px-6 py-4 whitespace-nowrap">
                                    <div class="flex items-center">
                                        <div class="w-8 h-8 bg-blue-500/10 rounded flex items-center justify-center mr-3">
                                            <i class="fas fa-server text-blue-500 text-sm"></i>
                                        </div>
                                        <span class="text-sm font-medium text-white"><?= htmlspecialchars($server['server_name']) ?></span>
                                    </div>
                                </td>
                                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-400"><?= htmlspecialchars($server['server_ip']) ?></td>
                                <td class="px-6 py-4 whitespace-nowrap">
                                    <code class="text-xs bg-dark-300 px-2 py-1 rounded text-gray-300"><?= substr($server['api_key'], 0, 16) ?>...</code>
                                </td>
                                <td class="px-6 py-4 whitespace-nowrap">
                                    <span class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full <?= $server['is_active'] ? 'bg-green-900/30 text-green-400' : 'bg-red-900/30 text-red-400' ?>">
                                        <?= $server['is_active'] ? 'Actief' : 'Inactief' ?>
                                    </span>
                                </td>
                                <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-400"><?= date('d-m-Y', strtotime($server['created_at'])) ?></td>
                                <td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
                                    <a href="/admin/servers/<?= $server['id'] ?>/edit" class="text-blue-400 hover:text-blue-300 mr-3">
                                        <i class="fas fa-edit"></i>
                                    </a>
                                    <form method="POST" action="/admin/servers/<?= $server['id'] ?>/toggle" class="inline mr-2">
                                        <button type="submit" class="text-yellow-400 hover:text-yellow-300">
                                            <i class="fas fa-power-off"></i>
                                        </button>
                                    </form>
                                    <form method="POST" action="/admin/servers/<?= $server['id'] ?>/delete" class="inline" 
                                          onsubmit="return confirm('Weet je zeker dat je deze server wilt verwijderen?')">
                                        <button type="submit" class="text-red-400 hover:text-red-300">
                                            <i class="fas fa-trash"></i>
                                        </button>
                                    </form>
                                </td>
                            </tr>
                        <?php endforeach; ?>
                    <?php endif; ?>
                </tbody>
            </table>
        </div>
        </div>
    </div>
</div>

<?php require_once __DIR__ . '/../includes/footer.php'; ?>
