<template>
    <v-layout align-start>
      <v-flex>
      <v-toolbar flat color="white">
        <v-toolbar-title>Sintomas y su medicamentos</v-toolbar-title>
      
        
      </v-toolbar>
      <v-row>
      <v-col
          class="d-flex"
          cols="12"
          sm="4"
        >
          <select
            label="Seleccione un medicamento"
            style="background-color: #80cbc4; margin: auto"

            dense
            outlined
          > <option v-for="medicamento in medicamentos" :key="medicamento.idMedicamento" :value="medicamento.idMedicamento">
            {{medicamento.nombre}}
        </option></select>
        </v-col>
        
        <v-col
          class="d-flex"
          cols="12"
          sm="6"
        >
          <select
            label="Seleccione un Sintoma"
            style=" background-color:#80cbc4; margin: auto "
            dense
          > <option v-for="sintoma in sintomas" :key="sintoma.idSintoma" :value="sintoma.idSintoma">
            {{sintoma.tipoMalestar+ '  ' + sintoma.lugarSintoma}}
        </option></select>
        </v-col>
    </v-row>
      </v-flex>
    </v-layout>
  </template>
  
  <script>
  import axios from 'axios';
  export default {
    data() {
      return {
        sintomas: [],
        dialog: false,
        dialogDelete: false,
        dialogActivar:false,
        dialogDesactivar: false,
        medicamentos:[],
        
        headers: [
          { text: "#", value: "idSintoma", sortable: false, align:"start"},
          { text: "Lugar del Sintoma", value: "lugarSintoma"},
          { text: "Tipo de Malestar", value: "tipoMalestar" },
          { text: "Estado", value: "actions2"},
          { text: "Opciones", value: "actions", sortable: false },
        ],
        search: '',
        editedIndex: -1,
          idSintoma:0,
          LugarSintoma: "",
          TipoMalestar:"",
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
      
    },
    methods: {
      Listar() {
        let me = this;
        let header={"Authorization" : "Bearer " + this.$store.state.token};
        let configuracion= {headers : header};
        axios.get("/api/Medicamentos/Listar",configuracion)
          .then(function (response) {
            console.log(response);
            me.medicamentos = response.data;
            
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
            console.log(response);
            me.sintomas = response.data;
          })
          .catch(function (error) {
            console.log(error);
          });
      },
      initialize() {
      this.medicamentos = [];
      this.sintomas = [];
      },
      //select(){
      //let me=this;
      //var rolesArray=[];
      //axios.get('api/Roles/Select'),then(function(response){
       //  rolesArray=response.data;
       //  rolesArray.map(function(x){
       //   me.roles.push({value:x.idRolUsuarios});
       //   console.log(rolesArray);
       //   });
       // }).catch(function(error){
       // console.log(error);
       // });
   // },
  
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
        this.idSintoma = item.idSintoma;
        this.dialogDelete = true;
      },
      DesactivarItem(item) {
        this.editedIndex = this.sintomas.indexOf(item);
        this.editedItem = Object.assign({}, item);
        this.idSintoma = item.idSintoma;
        this.dialogDesactivar = true;
      },
      ActivarItem(item) {
        this.editedIndex = this.sintomas.indexOf(item);
        this.editedItem = Object.assign({}, item);
        this.idSintoma = item.idSintoma;
        this.dialogActivar = true;
      },
  
      deleteItemConfirm() {
        //Codigo para editar
        let me=this;
          console.log(this);
          axios.delete('/api/Sintomas/Eliminar/'+me.idSintoma)
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
          axios.put('/api/Sintomas/Desactivar/'+me.idSintoma)
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
          axios.put('/api/Sintomas/Activar/'+me.idSintoma)
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
        this.idSintoma="";
        this.LugarSintoma="";
        this.TipoMalestar="";
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
          axios.put('/api/Sintomas/Actualizar',{
            'idSintoma':parseInt(me.idSintoma),
            'LugarSintoma': me.LugarSintoma,
            'TipoMalestar': me.TipoMalestar,
  
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
          axios.post('/api/Sintomas/Crear',{
            'LugarSintoma': me.LugarSintoma,
            'TipoMalestar': me.TipoMalestar,
  
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