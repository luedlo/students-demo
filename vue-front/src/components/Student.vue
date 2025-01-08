<template>
    <div class="container text-center">
      <h1 class="display-1">{{ title }}</h1>
      <div class="row">
        <h1 class="display-6 float-end">Búsqueda por Matrícula:</h1>
      </div>
      <div class="row offset-3 col-6 center">
        <div class="input-group mb-3">
          <input v-model="matricula" type="text" class="form-control" placeholder="Matrícula" aria-label="Matricula"
            aria-describedby="button-addon2" id="matriculaStudent">
          <button class="btn btn-outline-primary" type="button" id="button-addon2"
            v-on:click="getStudents">Buscar</button>
        </div>
      </div>

      <hr />

      <div class="row">
        <div class="col fw-bold">
          Matrícula
        </div>
        <div class="col fw-bold">
          Tipo de Pago
        </div>
        <div class="col fw-bold">
          Institución
        </div>
      </div>

      <hr />

      <div class="row" v-for="(data, index) in students" v-bind:key="index">
        <div class="col">
          {{ data.matricula }}
        </div>
        <div class="col">
          {{ data.tipoPago }}
        </div>
        <div class="col">
          {{ data.institucion }}
        </div>
      </div>
    </div>
</template>

<script>
import axios from "axios";

export default {
  name: 'StudentComponent',
  props: {
    title: String
  },
  data() {
    return {
      matricula: '',
      students: []
    }
  },
  methods: {
    getStudents() {
      axios.get(`http://localhost:5112/api/Students/${this.matricula}`)
        .then(response => {
          this.students = response.data
        })
        .catch(error => {
          if (error.response.status == 404) {
            this.students = []
            alert('No se encontraron resultados!')
          } else {
            alert('Error en el servicio!')
          }
        })
    }
  }
}
</script>

<!-- Add "scoped" attribute to limit CSS to this component only -->
<style scoped>
.btn-outline-primary {
  color: #42b983;
  text-shadow: 0px 0px 3px rgba(0, 0, 0, 0.1);
  border-color: #42b983;
}

.btn-outline-primary:hover {
  color: white;
  border-color: black;
  background-color: #42b983;
}
</style>
