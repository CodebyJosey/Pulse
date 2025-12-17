<!DOCTYPE html>
<html lang="nl">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title>Profiel - Pulse Dashboard</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
</head>
<body class="bg-gray-100">
    <!-- Navigation -->
    <nav class="bg-white shadow-lg">
        <div class="max-w-7xl mx-auto px-4">
            <div class="flex justify-between items-center py-4">
                <div class="flex items-center space-x-4">
                    <a href="/" class="text-2xl font-bold text-purple-600">Pulse Dashboard</a>
                </div>
                <div class="flex items-center space-x-4">
                    <span class="text-gray-700">
                        <i class="fas fa-user mr-2"></i>
                        <?= htmlspecialchars($user['username']) ?>
                    </span>
                    <a href="/logout" class="bg-red-500 hover:bg-red-600 text-white px-4 py-2 rounded-lg">
                        <i class="fas fa-sign-out-alt mr-2"></i>Uitloggen
                    </a>
                </div>
            </div>
        </div>
    </nav>

    <!-- Main Content -->
    <div class="max-w-4xl mx-auto px-4 py-8">
        <h2 class="text-3xl font-bold text-gray-800 mb-8">Mijn Profiel</h2>

        <div class="bg-white rounded-lg shadow-md p-8">
            <div class="flex items-center mb-6 pb-6 border-b">
                <div class="w-20 h-20 bg-purple-600 rounded-full flex items-center justify-center text-white text-3xl font-bold">
                    <?= strtoupper(substr($user['username'], 0, 1)) ?>
                </div>
                <div class="ml-6">
                    <h3 class="text-2xl font-bold text-gray-800"><?= htmlspecialchars($user['username']) ?></h3>
                    <p class="text-gray-600"><?= htmlspecialchars($user['email']) ?></p>
                    <span class="inline-block mt-2 px-3 py-1 bg-<?= $user['role'] === 'admin' ? 'red' : 'blue' ?>-100 text-<?= $user['role'] === 'admin' ? 'red' : 'blue' ?>-800 rounded-full text-sm font-semibold">
                        <?= ucfirst($user['role']) ?>
                    </span>
                </div>
            </div>

            <div class="space-y-4">
                <div>
                    <label class="block text-sm font-medium text-gray-700 mb-2">Gebruikersnaam</label>
                    <input type="text" value="<?= htmlspecialchars($user['username']) ?>" disabled 
                           class="w-full px-4 py-2 border border-gray-300 rounded-lg bg-gray-50">
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-700 mb-2">Email</label>
                    <input type="email" value="<?= htmlspecialchars($user['email']) ?>" disabled 
                           class="w-full px-4 py-2 border border-gray-300 rounded-lg bg-gray-50">
                </div>

                <div>
                    <label class="block text-sm font-medium text-gray-700 mb-2">Account aangemaakt op</label>
                    <input type="text" value="<?= date('d-m-Y H:i', strtotime($user['created_at'])) ?>" disabled 
                           class="w-full px-4 py-2 border border-gray-300 rounded-lg bg-gray-50">
                </div>
            </div>

            <div class="mt-8 flex space-x-4">
                <a href="/" class="bg-gray-500 hover:bg-gray-600 text-white px-6 py-2 rounded-lg">
                    <i class="fas fa-arrow-left mr-2"></i>Terug naar Dashboard
                </a>
            </div>
        </div>
    </div>
</body>
</html>
