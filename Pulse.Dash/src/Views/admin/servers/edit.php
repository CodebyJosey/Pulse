<?php 
$pageTitle = 'Server Bewerken';
require_once __DIR__ . '/../includes/header.php'; 
require_once __DIR__ . '/../includes/sidebar.php'; 
?>

<div class="bg-dark-100 rounded-lg p-6">
    <div class="flex justify-between items-center mb-6">
        <h2 class="text-2xl font-bold">Server Bewerken</h2>
        <a href="/admin/servers" class="text-blue-400 hover:text-blue-300">
            <i class="fas fa-arrow-left mr-2"></i>Terug
        </a>
    </div>

    <?php if (isset($_SESSION['error'])): ?>
        <div class="bg-red-500/10 border border-red-500 text-red-500 px-4 py-3 rounded mb-4">
            <?= htmlspecialchars($_SESSION['error']) ?>
        </div>
        <?php unset($_SESSION['error']); ?>
    <?php endif; ?>

    <form method="POST" action="/admin/servers/<?= $server['id'] ?>/edit" class="space-y-4">
        <div>
            <label class="block text-gray-300 mb-2">Server Naam</label>
            <input 
                type="text" 
                name="server_name" 
                value="<?= htmlspecialchars($server['server_name']) ?>"
                class="w-full bg-dark-200 border border-dark-300 rounded px-4 py-2 text-white focus:outline-none focus:border-blue-500"
                required
            >
        </div>

        <div>
            <label class="block text-gray-300 mb-2">Server IP</label>
            <input 
                type="text" 
                name="server_ip" 
                value="<?= htmlspecialchars($server['server_ip']) ?>"
                class="w-full bg-dark-200 border border-dark-300 rounded px-4 py-2 text-white focus:outline-none focus:border-blue-500"
                required
            >
        </div>

        <div class="flex items-center">
            <input 
                type="checkbox" 
                name="is_active" 
                id="is_active"
                <?= $server['is_active'] ? 'checked' : '' ?>
                class="mr-2"
            >
            <label for="is_active" class="text-gray-300">Actief</label>
        </div>

        <div class="flex gap-4">
            <button 
                type="submit" 
                class="bg-blue-600 hover:bg-blue-700 text-white px-6 py-2 rounded transition"
            >
                <i class="fas fa-save mr-2"></i>Opslaan
            </button>
            <a 
                href="/admin/servers" 
                class="bg-dark-200 hover:bg-dark-300 text-white px-6 py-2 rounded transition inline-block"
            >
                Annuleren
            </a>
        </div>
    </form>
</div>

<?php require_once __DIR__ . '/../includes/footer.php'; ?>
