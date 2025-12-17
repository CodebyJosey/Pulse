<?php 
$pageTitle = 'Nieuw Rapport';
require_once __DIR__ . '/../includes/header.php'; 
require_once __DIR__ . '/../includes/sidebar.php'; 
?>

<!-- Main Content -->
<div class="flex-1 flex flex-col overflow-hidden">
    <div class="flex-1 overflow-y-auto">
        <div class="p-6 max-w-4xl">
                    <div class="mb-6">
                        <h2 class="text-2xl font-bold text-white">Nieuw Rapport Maken</h2>
                        <p class="text-gray-400 mt-1 text-sm">Genereer een nieuw systeem rapport met huidige statistieken</p>
                    </div>

                    <?php if (isset($_SESSION['error'])): ?>
                        <div class="bg-red-900/30 border border-red-500 text-red-400 px-4 py-3 rounded-lg mb-4">
                            <?= htmlspecialchars($_SESSION['error']) ?>
                            <?php unset($_SESSION['error']); ?>
                        </div>
                    <?php endif; ?>

                    <div class="grid grid-cols-1 lg:grid-cols-2 gap-6 mb-6">
                        <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
                            <h3 class="text-lg font-bold text-white mb-4">Rapport Instellingen</h3>
                            <form method="POST" action="/admin/reports/create" class="space-y-4">
                                <div>
                                    <label class="block text-sm font-medium text-gray-300 mb-2">Titel</label>
                                    <input 
                                        type="text" 
                                        name="title" 
                                        required
                                        class="w-full px-4 py-2 bg-dark-200 border border-gray-700 rounded-lg text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                                        placeholder="bijv. Maandelijkse Status Rapport"
                                    >
                                </div>

                                <div>
                                    <label class="block text-sm font-medium text-gray-300 mb-2">Beschrijving</label>
                                    <textarea 
                                        name="description" 
                                        rows="3"
                                        class="w-full px-4 py-2 bg-dark-200 border border-gray-700 rounded-lg text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                                        placeholder="Optionele beschrijving van het rapport"
                                    ></textarea>
                                </div>

                                <div>
                                    <label class="block text-sm font-medium text-gray-300 mb-2">Type Rapport</label>
                                    <select 
                                        name="report_type"
                                        class="w-full px-4 py-2 bg-dark-200 border border-gray-700 rounded-lg text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                                    >
                                        <option value="overview">Algemeen Overzicht</option>
                                        <option value="users">Gebruikers Rapport</option>
                                        <option value="servers">Servers Rapport</option>
                                        <option value="custom">Aangepast</option>
                                    </select>
                                </div>

                                <div class="flex space-x-4 pt-4">
                                    <button 
                                        type="submit"
                                        class="flex-1 bg-blue-600 hover:bg-blue-700 text-white px-6 py-2 rounded-lg font-medium transition"
                                    >
                                        <i class="fas fa-save mr-2"></i>Rapport Opslaan
                                    </button>
                                    <a 
                                        href="/admin/reports"
                                        class="flex-1 bg-gray-600 hover:bg-gray-700 text-white px-6 py-2 rounded-lg font-medium transition text-center"
                                    >
                                        <i class="fas fa-times mr-2"></i>Annuleren
                                    </a>
                                </div>
                            </form>
                        </div>

                        <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
                            <h3 class="text-lg font-bold text-white mb-4">Huidige Statistieken</h3>
                            <div class="space-y-3">
                                <div class="bg-dark-200 rounded-lg p-4">
                                    <p class="text-gray-400 text-xs mb-1">Totaal Gebruikers</p>
                                    <p class="text-2xl font-bold text-white"><?= $stats['total_users'] ?></p>
                                </div>
                                <div class="bg-dark-200 rounded-lg p-4">
                                    <p class="text-gray-400 text-xs mb-1">Totaal Servers</p>
                                    <p class="text-2xl font-bold text-white"><?= $stats['total_servers'] ?></p>
                                </div>
                                <div class="bg-dark-200 rounded-lg p-4">
                                    <p class="text-gray-400 text-xs mb-1">Actieve Servers</p>
                                    <p class="text-2xl font-bold text-white"><?= $stats['active_servers'] ?></p>
                                </div>
                                <div class="bg-dark-200 rounded-lg p-4">
                                    <p class="text-gray-400 text-xs mb-1">Configuraties</p>
                                    <p class="text-2xl font-bold text-white"><?= $stats['total_configs'] ?></p>
                                </div>
                            </div>
                        </div>
                    </div>

                    <div class="bg-blue-900/20 border border-blue-500/30 rounded-lg p-4">
                        <div class="flex items-start">
                            <i class="fas fa-info-circle text-blue-400 mt-1 mr-3"></i>
                            <div>
                                <p class="text-blue-300 text-sm">
                                    Het rapport wordt opgeslagen met de huidige statistieken en kan later als PDF geëxporteerd worden.
                                    De gegevens worden vastgelegd op het moment van aanmaken.
                                </p>
                            </div>
                        </div>
                    </div>
                </div>
        </div>
    </div>
</div>

<?php require_once __DIR__ . '/../includes/footer.php'; ?>
