const cacheName = 'volt-gymcoach-v1';
const offlineAssets = [
  './',
  './index.html',
  './manifest.webmanifest',
  './css/app.css'
];

self.addEventListener('install', event => {
  event.waitUntil(
    caches.open(cacheName).then(cache => cache.addAll(offlineAssets))
  );
  self.skipWaiting();
});

self.addEventListener('activate', event => {
  event.waitUntil(self.clients.claim());
});

self.addEventListener('fetch', event => {
  event.respondWith(
    caches.match(event.request).then(cached => cached || fetch(event.request))
  );
});
