<?php 
$pageTitle = 'Server Toevoegen';
require_once __DIR__ . '/../includes/header.php'; 
require_once __DIR__ . '/../includes/sidebar.php'; 
?>

<!-- Main Content -->
<div class="flex-1 flex flex-col overflow-hidden">
    <div class="flex-1 overflow-y-auto">
        <div class="p-6 max-w-3xl">
        <div class="mb-6">
            <h2 class="text-2xl font-bold text-white">Nieuwe Server Toevoegen</h2>
            <p class="text-gray-400 mt-1 text-sm">Voeg een nieuwe Minecraft server toe aan het systeem</p>
        </div>

        <?php if (isset($_SESSION['error'])): ?>
            <div class="bg-red-900/30 border border-red-500 text-red-400 px-4 py-3 rounded-lg mb-4">
                <?= htmlspecialchars($_SESSION['error']) ?>
                <?php unset($_SESSION['error']); ?>
            </div>
        <?php endif; ?>

        <div class="bg-dark-100 border border-gray-800 rounded-lg p-6">
            <form method="POST" action="/admin/servers/create" class="space-y-6">
                <div>
                    <label for="server_name" class="block text-sm font-medium text-gray-300 mb-2">
                        Server Naam
                    </label>
                    <input 
                        type="text" 
                        id="server_name" 
                        name="server_name" 
                        required
                        class="w-full px-4 py-2 bg-dark-200 border border-gray-700 rounded-lg text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                        placeholder="bijv. Survival Server"
                    >
                </div>

                <div>
                    <label for="server_ip" class="block text-sm font-medium text-gray-300 mb-2">
                        Server IP Adres
                    </label>
                    <input 
                        type="text" 
                        id="server_ip" 
                        name="server_ip" 
                        required
                        class="w-full px-4 py-2 bg-dark-200 border border-gray-700 rounded-lg text-white focus:ring-2 focus:ring-blue-500 focus:border-transparent"
                        placeholder="bijv. play.example.com:25565"
                    >
                </div>

                <div class="bg-blue-900/20 border border-blue-500/30 rounded-lg p-4">
                    <div class="flex items-start">
                        <i class="fas fa-info-circle text-blue-400 mt-1 mr-3"></i>
                        <div>
                            <p class="text-blue-300 text-sm">
                                Een unieke API key wordt automatisch gegenereerd voor deze server. 
                                Deze key wordt gebruikt om de Minecraft plugin te authenticeren.
                            </p>
                        </div>
                    </div>
                </div>

                <div class="flex space-x-4">
                    <button 
                        type="submit"
                        class="bg-blue-600 hover:bg-blue-700 text-white px-6 py-2 rounded-lg font-medium transition"
                    >
                        <i class="fas fa-save mr-2"></i>Server Toevoegen
                    </button>
                    <a 
                        href="/admin/servers"
                        class="bg-gray-600 hover:bg-gray-700 text-white px-6 py-2 rounded-lg font-medium transition"
                    >
                        <i class="fas fa-times mr-2"></i>Annuleren
                    </a>
                </div>
            </form>
        </div>
        </div>
    </div>
</div>

<?php require_once __DIR__ . '/../includes/footer.php'; ?>
