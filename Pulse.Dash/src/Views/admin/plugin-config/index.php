<?php 
$pageTitle = 'Plugin Configuratie';
require_once __DIR__ . '/../includes/header.php'; 
require_once __DIR__ . '/../includes/sidebar.php'; 
?>

<!-- Main Content -->
<div class="flex-1 flex flex-col overflow-hidden">
    <div class="flex-1 overflow-y-auto">
        <div class="p-6 max-w-5xl">
        <div class="mb-6">
            <h2 class="text-2xl font-bold text-white">Plugin Configuratie</h2>
            <p class="text-gray-400 mt-1 text-sm">Configureer de Minecraft plugin instellingen</p>
        </div>

        <?php if (isset($_SESSION['success'])): ?>
            <div class="bg-green-900/30 border border-green-500 text-green-400 px-4 py-3 rounded-lg mb-4">
                <?= htmlspecialchars($_SESSION['success']) ?>
                <?php unset($_SESSION['success']); ?>
            </div>
        <?php endif; ?>

        <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
            <form method="POST" action="/admin/plugin-config" class="space-y-6">
                <?php foreach ($configs as $config): ?>
                <div class="border-b border-gray-800 pb-6 last:border-b-0 last:pb-0">
                    <div class="flex items-start justify-between">
                        <div class="flex-1">
                            <label class="block text-sm font-bold text-white mb-1">
                                <?= htmlspecialchars(ucfirst(str_replace('_', ' ', $config['config_key']))) ?>
                            </label>
                            <?php if ($config['description']): ?>
                            <p class="text-sm text-gray-400 mb-3">
                                <?= htmlspecialchars($config['description']) ?>
                            </p>
                            <?php endif; ?>
                            
                            <?php if (in_array($config['config_key'], ['enable_whitelist'])): ?>
                                <select 
                                    name="config_<?= $config['id'] ?>"
                                    class="w-full px-4 py-2 bg-dark-200 border border-gray-700 rounded-lg text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                                >
                                    <option value="true" <?= $config['config_value'] === 'true' ? 'selected' : '' ?>>Aan</option>
                                    <option value="false" <?= $config['config_value'] === 'false' ? 'selected' : '' ?>>Uit</option>
                                </select>
                            <?php else: ?>
                                <input 
                                    type="text" 
                                    name="config_<?= $config['id'] ?>" 
                                    value="<?= htmlspecialchars($config['config_value']) ?>"
                                    class="w-full px-4 py-2 bg-dark-200 border border-gray-700 rounded-lg text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                                >
                            <?php endif; ?>
                        </div>
                    </div>
                </div>
                <?php endforeach; ?>

                <div class="pt-4">
                    <button 
                        type="submit"
                        class="bg-blue-600 hover:bg-blue-700 text-white px-8 py-3 rounded-lg font-semibold transition"
                    >
                        <i class="fas fa-save mr-2"></i>Configuratie Opslaan
                    </button>
                </div>
            </form>
        </div>

        <div class="mt-6 bg-blue-900/20 border border-blue-500/30 rounded-lg p-6">
            <div class="flex items-start">
                <i class="fas fa-info-circle text-blue-400 text-xl mt-1 mr-3"></i>
                <div>
                    <h4 class="text-lg font-semibold text-blue-300 mb-2">Minecraft Plugin Integratie</h4>
                    <p class="text-blue-300 text-sm">
                        Deze instellingen worden gebruikt door de Minecraft plugin om verbinding te maken met de API. 
                        Zorg ervoor dat het API endpoint correct is geconfigureerd voordat je de plugin activeert.
                    </p>
                </div>
            </div>
        </div>
        </div>
    </div>
</div>

<?php require_once __DIR__ . '/../includes/footer.php'; ?>
