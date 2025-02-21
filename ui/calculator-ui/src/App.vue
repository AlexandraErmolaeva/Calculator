<template>
  <div id="app">
    <nav>
      <router-link to="/" class="nav-link">Калькулятор операций</router-link>
      <router-link to="/expression" class="nav-link">Калькулятор выражений</router-link>
    </nav>
    <router-view />
  </div>
</template>

<style>
#app {
  font-family: 'Arial', sans-serif;
  color: #333; 
  padding: 10px;
  text-align: center; 
}

.nav-link {
  margin: 0 20px;
  margin-bottom: 20px; 
  font-size: 18px;
  text-decoration: none;
  color: #079cb6;
  font-weight: 500;
  transition: color 0.3s ease;
}

.nav-link:hover {
  color: #047693;
}

</style>
