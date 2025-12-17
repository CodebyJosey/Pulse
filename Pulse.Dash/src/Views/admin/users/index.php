<?php 
$pageTitle = 'Gebruikersbeheer';
require_once __DIR__ . '/../includes/header.php'; 
require_once __DIR__ . '/../includes/sidebar.php'; 
?>

<!-- Main Content -->
<div class="flex-1 flex flex-col overflow-hidden">
    <div class="flex-1 overflow-y-auto">
        <div class="p-6">
        <div class="mb-6">
            <h2 class="text-2xl font-bold text-white">Gebruikersbeheer</h2>
            <p class="text-gray-400 mt-1 text-sm">Beheer alle gebruikers van het systeem</p>
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
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">Gebruikersnaam</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">Email</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">Rol</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">Aangemaakt</th>
                        <th class="px-6 py-3 text-left text-xs font-medium text-gray-400 uppercase tracking-wider">Acties</th>
                    </tr>
                </thead>
                <tbody class="divide-y divide-gray-800">
                    <?php foreach ($users as $u): ?>
                    <tr class="hover:bg-dark-200/50">
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-white"><?= $u['id'] ?></td>
                        <td class="px-6 py-4 whitespace-nowrap">
                            <div class="flex items-center">
                                <div class="w-8 h-8 bg-purple-500/10 rounded-full flex items-center justify-center mr-3">
                                    <span class="text-purple-400 text-sm font-bold"><?= strtoupper(substr($u['username'], 0, 1)) ?></span>
                                </div>
                                <span class="text-sm font-medium text-white">
                                    <?= htmlspecialchars($u['username']) ?>
                                </span>
                            </div>
                        </td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-400">
                            <?= htmlspecialchars($u['email']) ?>
                        </td>
                        <td class="px-6 py-4 whitespace-nowrap">
                            <span class="px-2 inline-flex text-xs leading-5 font-semibold rounded-full 
                                <?= $u['role'] === 'admin' ? 'bg-red-900/30 text-red-400' : 'bg-blue-900/30 text-blue-400' ?>">
                                <?= ucfirst($u['role']) ?>
                            </span>
                        </td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm text-gray-400">
                            <?= date('d-m-Y', strtotime($u['created_at'])) ?>
                        </td>
                        <td class="px-6 py-4 whitespace-nowrap text-sm font-medium">
                            <a href="/admin/users/<?= $u['id'] ?>" class="text-blue-400 hover:text-blue-300 mr-3">
                                <i class="fas fa-edit"></i>
                            </a>
                            <?php if ($u['id'] != $_SESSION['user_id']): ?>
                            <form method="POST" action="/admin/users/<?= $u['id'] ?>/delete" class="inline" 
                                  onsubmit="return confirm('Weet je zeker dat je deze gebruiker wilt verwijderen?')">
                                <button type="submit" class="text-red-400 hover:text-red-300">
                                    <i class="fas fa-trash"></i>
                                </button>
                            </form>
                            <?php endif; ?>
                        </td>
                    </tr>
                    <?php endforeach; ?>
                </tbody>
            </table>
        </div>
        </div>
    </div>
</div>

<?php require_once __DIR__ . '/../includes/footer.php'; ?>
