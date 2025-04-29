<template>
  <div id="app">
    <!-- Navigation Bar -->
    <nav class="navbar navbar-expand-lg navbar-light bg-light">
      <div class="container-fluid">
        <a class="navbar-brand" href="#">HealthApp</a>
        <div class="navbar-nav">
          <router-link class="nav-link" to="/health-records">Записи о здоровье</router-link>
          <router-link class="nav-link" to="/biomarkers">Биомаркеры</router-link>
          <router-link class="nav-link" to="/medications">Медикаменты</router-link>
          <router-link class="nav-link" to="/documents">Документы</router-link>
        </div>
      </div>
    </nav>

    <!-- Main Content -->
    <div class="container mt-4">
      <router-view></router-view>
    </div>
  </div>
</template>

<script>
export default {
  name: 'App',
};
</script>

<style>
#app {
  font-family: Avenir, Helvetica, Arial, sans-serif;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
  text-align: center;
  color: #2c3e50;
  margin-top: 60px;
}
</style>