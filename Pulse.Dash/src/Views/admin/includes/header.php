<!DOCTYPE html>
<html lang="nl" class="dark">
<head>
    <meta charset="UTF-8">
    <meta name="viewport" content="width=device-width, initial-scale=1.0">
    <title><?= $pageTitle ?? 'Admin Panel' ?> - Pulse</title>
    <script src="https://cdn.tailwindcss.com"></script>
    <?php if (isset($includeChartJs) && $includeChartJs): ?>
    <script src="https://cdn.jsdelivr.net/npm/chart.js"></script>
    <?php endif; ?>
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/6.4.0/css/all.min.css">
    <script>
        tailwind.config = {
            darkMode: 'class',
            theme: {
                extend: {
                    colors: {
                        dark: {
                            50: '#0c0a09',
                            100: '#1c1917',
                            200: '#292524',
                            300: '#44403c',
                        }
                    }
                }
            }
        }
    </script>
</head>
<body class="bg-dark-50 text-gray-100">
    <div class="flex h-screen overflow-hidden">
