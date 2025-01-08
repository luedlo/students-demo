<template>
  <div id="app">
    <div class="container mx-auto">
      <img alt="Vue logo" src="./assets/logo.png" width="10%" class="mx-auto">
      <Student title="Sistema de Consultas v2.0" />
    </div>
  </div>
</template>

<script>
import Student from './components/Student.vue'

export default {
  name: 'App',
  components: {
    Student
  }
}
</script>

<style>
body {
  background-color: transparent !important;
}

html {
  width: 100%;
  height: 100%;
  background-image: linear-gradient(to top, #94ffdd 0%, #fcfcfc 100%);
}

#app {
  text-align: center;
  color: #2c3e50;
  background: rgba(0, 0, 0, 0);
  margin-top: 60px;
}
</style>
