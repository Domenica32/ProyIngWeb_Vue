<template>
    <v-layout align-start>
      <v-flex>
      <v-toolbar flat color="white">
        <v-toolbar-title>Sintomas y su medicamentos</v-toolbar-title>
        <v-divider
        class="mx-2"
        inset  
        vertical ></v-divider>
        <v-spacer></v-spacer>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-wiidth="500px">
          <template v-slot:activator="{ on, attrs }">
          <v-btn color="teal lighten-3" dark class="mb-2" v-bind="attrs" v-on="on" @click="CrearRelacion">
            
          Agregar Sintoma a un medicamento
          </v-btn>

          </template></v-dialog>
        
      </v-toolbar>
      
      <v-spacer></v-spacer>
      <v-spacer></v-spacer>
      <v-spacer></v-spacer>

      <v-row>
      <v-col
          class="d-flex"
          cols="12"
          sm="4"
        >
        
          <select
            label="Seleccione un medicamento"
            v-model="select"
            style="background-color: #80cbc4; margin: auto"
            placeholder="Seleccione un medicamento"
            dense
            outlined
          > <option v-for="medicamento in medicamentos" :key="medicamento.idMedicamento" :value="medicamento.idMedicamento" >
             {{medicamento.nombre}}
        </option></select> 
    
        <!-- <p>Seleccion {{select}}</p> -->
        </v-col>
        
        <v-col
          class="d-flex"
          cols="12"
          sm="6"
        >
          <select
            label="Seleccione un Sintoma"
            v-model="select2"
            style=" background-color:#80cbc4; margin: auto "
            dense
          > <option v-for="sintoma in sintomas" :key="sintoma.idSintoma" :value="sintoma.idSintoma">
            {{sintoma.tipoMalestar+ '  ' + sintoma.lugarSintoma}}
        </option></select>
        <!-- <p>Seleccion: {{select2}}</p> -->
        </v-col>
    </v-row>
    <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Busqueda" single-line hide-details></v-text-field>
<v-spacer></v-spacer>
<v-dialog v-model="dialogDelete" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">Estas seguro que deseas eliminarlo?</v-card-title>
            <v-card-action>
              <v-spacer></v-spacer>
              <v-btn color="teal lighten-3" text  @click="closeDelete">Cancel</v-btn>
              <v-btn color="teal lighten-3" text  @click="deleteItemConfirm">Ok</v-btn>
              <v-spacer></v-spacer>
            </v-card-action>
          </v-card>
        </v-dialog>
    <v-data-table
          :headers="headers"
          :items="relacion"
          :search="search"
          class="elevation-1"
        >
        <template v-slot:[`item.actions`]="{ item }">
          
          
          <v-icon
            small
            @click="deleteItem(item)"
          >
            mdi-delete
          </v-icon>

      
        </template>
        <template v-slot:no-data>
          <v-btn
            color="primary"
            @click="initialize"
          >
            Reset
          </v-btn>
        </template>
        </v-data-table>
   
      </v-flex>
    </v-layout>
  </template>
  
  <script>
  import axios from 'axios';
  export default {
    data() {
      return {
        relacion:[],
        sintomas: [],
        select:null,
        select2:null,
        dialog: false,
        dialogDelete: false,
        dialogActivar:false,
        dialogDesactivar: false,
        medicamentos:[],
        
        headers: [
          { text: "Medicamento", value: "medicamento.nombre"},
          { text: "Tipo de Malestar", value: "sintoma.tipoMalestar" },
          { text: "Lugar Sintoma", value: "sintoma.lugarSintoma" },
          { text: "Opciones", value: "actions", sortable: false },

        ],
        search: '',
        editedIndex: -1,
        idMedicamento_Sintoma:0,
          nombre:"",
          lugarSintoma: "",
          tipoMalestar:"",
          valida:0,
          validaMensaje:[],
      };
    },
    computed: {
      formTitle() {
        return this.editedIndex === -1 ? 'Nuevo Sintoma' : 'Editar Sintoma';
      },
    },
    watch: {
      dialog(val) {
        val || this.close();
      },
      dialogDelete(val) {
        val || this.closeDelete();
      },
    },
    created() {
      this.initialize();
      this.Listar();
      this.ListarSintomas();
      this.ListarRelacion();
    },
    methods: {
      Listar() {
        let me = this;
        let header={"Authorization" : "Bearer " + this.$store.state.token};
        let configuracion= {headers : header};
        axios.get("/api/Medicamentos/Listar",configuracion)
          .then(function (response) {
            //console.log(response);
            me.medicamentos = response.data;
            
            
          })
          .catch(function (error) {
            console.log(error);
          });
      },
      initialize() {
      this.relacion =[];
      this.medicamentos = [];
      this.sintomas = [];
      },
      ListarRelacion() {
        let me = this;
        let header={"Authorization" : "Bearer " + this.$store.state.token};
        let configuracion= {headers : header};
        axios.get("/api/RelacionMedicamentoSintomas/Listar",configuracion)
          .then(function (response) {
            //console.log(response);
            me.relacion = response.data;
            
            
          })
          .catch(function (error) {
            console.log(error);
          });
      },
      ListarSintomas() {
        let me = this;
        let header={"Authorization" : "Bearer " + this.$store.state.token};
        let configuracion= {headers : header};
        axios.get("/api/Sintomas/Listar",configuracion)
          .then(function (response) {
           // console.log(response);
            me.sintomas = response.data;
          })
          .catch(function (error) {
            console.log(error);
          });
      },
      CrearRelacion(){
        let me=this;
          //console.log(this);
          axios.post('/api/RelacionMedicamentoSintomas/Crear',{
            'idMedicamento_FK': me.select,
            'idSintoma_FK': me.select2,
  
          }).then(function(response){
            me.close();
            me.ListarRelacion();
            me.limpiar();
            
          }).catch(function (error){
            console.log(error);
          });
        
      },
      
  
      editItem(item) {
        //this.editedIndex = this.desserts.indexOf(item);
        //this.editedItem = Object.assign({}, item);
        //console.log(this.editedItem);
        this.idSintoma = item.idSintoma;
        this.LugarSintoma = item.lugarSintoma;
        this.TipoMalestar = item.tipoMalestar;
        this.editedIndex=1;
        this.dialog = true;
      },
  
      deleteItem(item) {
        this.editedIndex = this.sintomas.indexOf(item);
        this.editedItem = Object.assign({}, item);
        this.idMedicamento_Sintoma = item.idMedicamento_Sintoma;
        this.dialogDelete = true;
      },
      close() {
        this.dialog = false;
        this.limpiar();
      },
  
      closeDelete() {
        this.dialogDelete = false;
        this.dialogDesactivar =false;
        this.dialogActivar =false;
        this.$nextTick(() => {
          this.editedItem = Object.assign({}, this.defaultItem);
          this.editedIndex = -1;
        });
      },
      limpiar(){
        this.idMedicamento_Sintoma="",
        this.nombre="",
        this.lugarSintoma= "",
        this.tipoMalestar="",
        this.editedIndex =-1;
      },
      deleteItemConfirm() {
        //Codigo para editar
        let me=this;
          //console.log(this);
          axios.delete('/api/RelacionMedicamentoSintomas/Eliminar/'+me.idMedicamento_Sintoma)
          .then(function(response){
            me.close();
            me.ListarRelacion();
            me.limpiar();
            
          }).catch(function (error){
            console.log(error);
          });
        this.closeDelete();
      },
      
      validar(){ //evaluamos que los campos cumplan con lo requerido 
        this.valida=0;
        this.validaMensaje=[];
  
        if(this.NombreUsuario.length<5|| this.NombreUsuario>30){
          this.validaMensaje.push("El nombre debe tener mas de 5 caracteres y menos de 30");
        }
        if(this.validaMensaje.length){
          this.valida=1;
        }
        return this.valida;
  
      },

    },
  };
  </script>