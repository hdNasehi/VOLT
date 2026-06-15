window.voltStorage = {
  getItem: function (key) {
    try { return localStorage.getItem(key); } catch { return null; }
  },
  setItem: function (key, value) {
    try { localStorage.setItem(key, value); } catch { }
  },
  removeItem: function (key) {
    try { localStorage.removeItem(key); } catch { }
  }
};
