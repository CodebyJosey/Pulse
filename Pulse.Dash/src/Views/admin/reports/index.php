<?php 
$pageTitle = 'Rapporten';
require_once __DIR__ . '/../includes/header.php'; 
require_once __DIR__ . '/../includes/sidebar.php'; 
?>

<!-- Main Content -->
<div class="flex-1 flex flex-col overflow-hidden">
    <div class="flex-1 overflow-y-auto">
        <div class="p-6">
                    <div class="mb-6 flex items-center justify-between">
                        <div>
                            <h2 class="text-2xl font-bold text-white">Rapporten</h2>
                            <p class="text-gray-400 mt-1 text-sm">Genereer en beheer systeem rapporten</p>
                        </div>
                        <a href="/admin/reports/create" class="px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded-lg font-medium transition">
                            <i class="fas fa-plus mr-2"></i>Nieuw Rapport
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

                    <?php if (empty($reports)): ?>
                        <div class="bg-dark-100 border border-gray-800 rounded-lg p-12 text-center">
                            <i class="fas fa-file-alt text-gray-600 text-6xl mb-4"></i>
                            <h3 class="text-xl font-bold text-white mb-2">Geen rapporten</h3>
                            <p class="text-gray-400 mb-4">Je hebt nog geen rapporten aangemaakt.</p>
                            <a href="/admin/reports/create" class="inline-block px-4 py-2 bg-blue-600 hover:bg-blue-700 text-white rounded-lg font-medium transition">
                                <i class="fas fa-plus mr-2"></i>Eerste Rapport Maken
                            </a>
                        </div>
                    <?php else: ?>
                        <div class="grid grid-cols-1 md:grid-cols-2 lg:grid-cols-3 gap-6">
                            <?php foreach ($reports as $report): ?>
                                <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
                                    <div class="flex items-start justify-between mb-4">
                                        <div class="flex-1">
                                            <h3 class="text-lg font-bold text-white mb-1"><?= htmlspecialchars($report['title']) ?></h3>
                                            <p class="text-gray-400 text-sm"><?= htmlspecialchars($report['description']) ?></p>
                                        </div>
                                        <span class="px-2 py-1 bg-blue-500/10 text-blue-400 text-xs rounded">
                                            <?= ucfirst($report['report_type']) ?>
                                        </span>
                                    </div>
                                    <div class="text-gray-500 text-xs mb-4">
                                        <i class="fas fa-calendar mr-1"></i>
                                        <?= date('d-m-Y H:i', strtotime($report['created_at'])) ?>
                                    </div>
                                    <div class="flex space-x-2">
                                        <a href="/admin/reports/<?= $report['id'] ?>/pdf" class="flex-1 bg-green-600 hover:bg-green-700 text-white px-3 py-2 rounded text-center text-sm font-medium transition">
                                            <i class="fas fa-file-pdf mr-1"></i>PDF
                                        </a>
                                        <form method="POST" action="/admin/reports/<?= $report['id'] ?>/delete" class="flex-1" 
                                              onsubmit="return confirm('Weet je zeker dat je dit rapport wilt verwijderen?')">
                                            <button type="submit" class="w-full bg-red-600 hover:bg-red-700 text-white px-3 py-2 rounded text-sm font-medium transition">
                                                <i class="fas fa-trash mr-1"></i>Verwijder
                                            </button>
                                        </form>
                                    </div>
                                </div>
                            <?php endforeach; ?>
                        </div>
                    <?php endif; ?>
                </div>
        </div>
    </div>
</div>

<?php require_once __DIR__ . '/../includes/footer.php'; ?>
