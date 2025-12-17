<!-- Admin Sidebar -->
<aside class="w-64 bg-dark-100 border-r border-gray-800 flex-shrink-0">
    <div class="p-6 border-b border-gray-800">
        <h1 class="text-xl font-bold flex items-center">
            <i class="fas fa-shield-alt mr-2 text-red-500"></i>
            <span class="bg-gradient-to-r from-red-400 to-orange-500 bg-clip-text text-transparent">Admin Panel</span>
        </h1>
    </div>
    <nav class="p-4 space-y-1">
        <a href="/admin" class="flex items-center px-4 py-3 <?= $_SERVER['REQUEST_URI'] == '/admin' ? 'bg-dark-200 text-red-400' : 'text-gray-400 hover:bg-dark-200 hover:text-white' ?> rounded-lg transition">
            <i class="fas fa-tachometer-alt mr-3 w-5"></i>Dashboard
        </a>
        <a href="/admin/users" class="flex items-center px-4 py-3 <?= strpos($_SERVER['REQUEST_URI'], '/admin/users') !== false ? 'bg-dark-200 text-red-400' : 'text-gray-400 hover:bg-dark-200 hover:text-white' ?> rounded-lg transition">
            <i class="fas fa-users mr-3 w-5"></i>Gebruikers
        </a>
        <a href="/admin/servers" class="flex items-center px-4 py-3 <?= strpos($_SERVER['REQUEST_URI'], '/admin/servers') !== false ? 'bg-dark-200 text-red-400' : 'text-gray-400 hover:bg-dark-200 hover:text-white' ?> rounded-lg transition">
            <i class="fas fa-server mr-3 w-5"></i>Servers
        </a>
        <a href="/admin/plugin-config" class="flex items-center px-4 py-3 <?= strpos($_SERVER['REQUEST_URI'], '/admin/plugin-config') !== false ? 'bg-dark-200 text-red-400' : 'text-gray-400 hover:bg-dark-200 hover:text-white' ?> rounded-lg transition">
            <i class="fas fa-cog mr-3 w-5"></i>Configuratie
        </a>
        <a href="/admin/reports" class="flex items-center px-4 py-3 <?= strpos($_SERVER['REQUEST_URI'], '/admin/reports') !== false ? 'bg-dark-200 text-red-400' : 'text-gray-400 hover:bg-dark-200 hover:text-white' ?> rounded-lg transition">
            <i class="fas fa-file-alt mr-3 w-5"></i>Rapporten
        </a>
    </nav>
    <div class="absolute bottom-0 w-64 p-4 border-t border-gray-800">
        <a href="/" class="flex items-center px-4 py-2 text-gray-400 hover:text-white text-sm transition">
            <i class="fas fa-home mr-2"></i>User Dashboard
        </a>
        <div class="flex items-center px-4 py-3 bg-dark-200 rounded-lg mt-2">
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
