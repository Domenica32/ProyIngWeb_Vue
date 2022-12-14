<template>
    <v-layout align-start>
      <v-flex>
      <v-toolbar flat color="white">
        <v-toolbar-title>Historial de sugerencias</v-toolbar-title>
        <v-divider
        class="mx-2"
        inset  
        vertical ></v-divider>
        <v-spacer></v-spacer>
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
       
        
      </v-toolbar>
  
        <v-data-table
          :headers="headers"
          :items="medicamentos"
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
        
        medicamentos: [],
        dialog: false,
        dialogDelete: false,
        dialogActivar:false,
        dialogDesactivar: false,
        headers: [
        { text: "id", value: "idSugerencia"},
          { text: "Nombre Medicamento", value: "medicamento.nombre"},
          { text: "Compuesto Quimico", value: "medicamento.compuestoQuimico"},
          { text: "Dosis", value: "medicamento.dosis" },
          //{ text: "Persona", value: "usuario.nombreUsuario" },
          { text: "Opciones", value: "actions", sortable: false },
        ],
        search: '',
        editedIndex: -1,
          idSugerencia:0,
          idMedicamento:0,
          Nombre: "",
          CompuestoQuimico:"",
          Dosis: "",
          valida:0,
          validaMensaje:[],
      };
    },
    computed: {
      formTitle() {
        return this.editedIndex === -1 ? 'Nuevo Medicamento' : 'Editar Medicamento';
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
      
    },
    methods: {
      Listar() {
        let me = this;
        let header={"Authorization" : "Bearer " + this.$store.state.token};
        let configuracion= {headers : header};
        axios.get("/api/SugerenciaMedicinas/Listar",configuracion)
          .then(function (response) {
            console.log(response);
            me.medicamentos = response.data;
          })
          .catch(function (error) {
            console.log(error);
          });
      },
      initialize() {
      this.medicamentos = [];
      
      },
      deleteItem(item) {
        this.editedIndex = this.medicamentos.indexOf(item);
        this.editedItem = Object.assign({}, item);
        this.idSugerencia = item.idSugerencia;
        this.dialogDelete = true;
      },
  
    
  
      deleteItemConfirm() {
        //Codigo para editar
        let me=this;
          console.log(this);
          axios.delete('/api/SugerenciaMedicinas/Eliminar/'+me.idSugerencia)
          .then(function(response){
            me.close();
            me.Listar();
            me.limpiar();
            
          }).catch(function (error){
            console.log(error);
          });
        this.closeDelete();
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
        this.idMedicamento="";
        this.Nombre="";
        this.CompuestoQuimico="";
        this.Dosis="";
        this.editedIndex =-1;
      },
      save() {
        
      // if (this.validar()){
        //  return; 
       // }
  
        if (this.editedIndex > -1) {
          //Codigo para editar
          let me=this;
          console.log(this);
          axios.put('/api/Medicamentos/Actualizar',{
            'idMedicamento':parseInt(me.idMedicamento),
            'Nombre': me.Nombre,
            'CompuestoQuimico': me.CompuestoQuimico,
            'Dosis': me.Dosis
  
          }).then(function(response){
            me.close();
            me.Listar();
            me.limpiar();
            
          }).catch(function (error){
            console.log(error);
          });
        } else {
          //codigo para guardar 
          let me=this;
          console.log(this);
          axios.post('/api/Medicamentos/Crear',{
            'Nombre': me.Nombre,
            'CompuestoQuimico': me.CompuestoQuimico,
            'Dosis': me.Dosis,
  
          }).then(function(response){
            me.close();
            me.Listar();
            me.limpiar();
            
          }).catch(function (error){
            console.log(error);
          });
        }
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