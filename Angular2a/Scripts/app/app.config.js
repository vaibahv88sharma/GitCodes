scripts = document.getElementsByTagName('script');
src = scripts[scripts.length - 1].src;
src = src.replace("/app.config.js", "");

var BASE_URL = src;

delete scripts;
delete src;