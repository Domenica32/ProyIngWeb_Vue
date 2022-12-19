<template>
    <v-layout align-start>
      <v-flex>
      <v-toolbar flat color="white">
        <v-toolbar-title>Medicamentos</v-toolbar-title>
        <v-divider
        class="mx-2"
        inset  
        vertical ></v-divider>
        <v-spacer></v-spacer>
        <v-text-field class="text-xs-center" v-model="search" append-icon="search" label="Busqueda" single-line hide-details></v-text-field>
        <v-spacer></v-spacer>
        <v-dialog v-model="dialog" max-wiidth="500px">
          <template v-slot:activator="{ on, attrs }">
          <v-btn color="teal lighten-3" dark class="mb-2" v-bind="attrs" v-on="on">
          Nuevo Medicamento
          </v-btn>
          </template>
        <v-card>
            <v-card-title>
              <span class="headline">{{ formTitle }}</span>
            </v-card-title>
  
            <v-card-text>
              <v-container >
                <v-row>
                  <!--Para llenar los campos del dialogo editar -->
                  <v-flex  xs12 sm12 md12>
                   <v-text-field v-model="Nombre" label="Nombre Medicamento"></v-text-field>
                   <!--<v-select v-model="idRolUsuarios_FK" :items="roles" label="Rol Usuario"></v-select>-->
                  </v-flex>
                  <v-flex  xs12 sm12 md12>
                    <v-text-field v-model="CompuestoQuimico" label="Compuesto Quimico"></v-text-field>
                  </v-flex>
                  <v-flex  xs12 sm12 md12>
                    <v-text-field v-model="Dosis" label="Dosis"></v-text-field>
                  </v-flex>
                  
                  <v-flex  xs12 sm12 md12 v-show="valida">
                    <div class="red--text" v-for="v in validaMensaje" :key="v" v-text="v">
                    </div>
                  </v-flex>
                </v-row>
              </v-container>
            </v-card-text>
            <v-card-actions>
              <v-spacer></v-spacer>
              <v-btn color="teal lighten-3" text  @click="close">Cancel</v-btn>
              <v-btn color="teal lighten-3" text  @click="save">Save</v-btn>
  
            </v-card-actions>
          </v-card> 
        </v-dialog>
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
        <v-dialog v-model="dialogActivar" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">Estas seguro que deseas Activar?</v-card-title>
            <v-card-action>
              <v-spacer></v-spacer>
              <v-btn color="teal lighten-3" text  @click="closeDelete">Cancel</v-btn>
              <v-btn color="teal lighten-3" text  @click="ActivarItemConfirm">Ok</v-btn>
              <v-spacer></v-spacer>
            </v-card-action>
          </v-card>
        </v-dialog>
        <v-dialog v-model="dialogDesactivar" max-width="500px">
          <v-card>
            <v-card-title class="text-h5">Estas seguro que deseas Desactivar?</v-card-title>
            <v-card-action>
              <v-spacer></v-spacer>
              <v-btn color="teal lighten-3" text  @click="closeDelete">Cancel</v-btn>
              <v-btn color="teal lighten-3" text  @click="DesactivarItemConfirm">Ok</v-btn>
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
        <template v-slot:[`item.actions2`]="{ item }">
            <td>
                        <div v-if="item.estado">
                            <span class="blue--text">Activo</span>
                        </div>
                        <div v-else>
                            <span class="red--text">Inactivo</span>
                        </div>
            </td>
        </template>

        <template v-slot:[`item.actions`]="{ item }">
          
          <v-icon
            small
            class="mr-2"
            @click="editItem(item)"
          >
            mdi-pencil
          </v-icon>
          <v-icon
            small
            @click="deleteItem(item)"
          >
            mdi-delete
          </v-icon>

        <template v-if="item.estado">
            <v-icon
            small
            @click="DesactivarItem(item)"
          >
            block
          </v-icon> 
        </template>
        <template v-else>
            <v-icon
            small
            @click="ActivarItem(item)"
          >
            check
          </v-icon> 
        </template>
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
          { text: "#", value: "idMedicamento", sortable: false, align:"start"},
          { text: "Nombre Medicamento", value: "nombre"},
          { text: "Compuesto Quimico", value: "compuestoQuimico" },
          { text: "Dosis", value: "dosis" },
          { text: "Estado", value: "actions2"},
          { text: "Opciones", value: "actions", sortable: false },
        ],
        search: '',
        editedIndex: -1,
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
      //this.ListarSintomas();
      
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
      this.medicamentos = [];
      
      },
      
      editItem(item) {
        //this.editedIndex = this.desserts.indexOf(item);
        //this.editedItem = Object.assign({}, item);
        //console.log(this.editedItem);
        this.idMedicamento = item.idMedicamento;
        this.Nombre = item.nombre;
        this.CompuestoQuimico = item.compuestoQuimico;
        this.Dosis = item.dosis;
        this.editedIndex=1;
        this.dialog = true;
      },
  
      deleteItem(item) {
        this.editedIndex = this.medicamentos.indexOf(item);
        this.editedItem = Object.assign({}, item);
        this.idMedicamento = item.idMedicamento;
        this.dialogDelete = true;
      },
      DesactivarItem(item) {
        this.editedIndex = this.medicamentos.indexOf(item);
        this.editedItem = Object.assign({}, item);
        this.idMedicamento = item.idMedicamento;
        this.dialogDesactivar = true;
      },
      ActivarItem(item) {
        this.editedIndex = this.medicamentos.indexOf(item);
        this.editedItem = Object.assign({}, item);
        this.idMedicamento = item.idMedicamento;
        this.dialogActivar = true;
      },
  
      deleteItemConfirm() {
        //Codigo para editar
        let me=this;
          //console.log(this);
          axios.delete('/api/Medicamentos/Eliminar/'+me.idMedicamento)
          .then(function(response){
            me.close();
            me.Listar();
            me.limpiar();
            
          }).catch(function (error){
            console.log(error);
          });
        this.closeDelete();
      },
      DesactivarItemConfirm() {
        //Codigo para editar
        let me=this;
          //console.log(this);
          axios.put('/api/Medicamentos/Desactivar/'+me.idMedicamento)
          .then(function(response){
            me.close();
            me.Listar();
            me.limpiar();
            
          }).catch(function (error){
            console.log(error);
          });
        this.closeDelete();
      },
      ActivarItemConfirm() {
        //Codigo para editar
        let me=this;
          //console.log(this);
          axios.put('/api/Medicamentos/Activar/'+me.idMedicamento)
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
          //console.log(this);
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
          //console.log(this);
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