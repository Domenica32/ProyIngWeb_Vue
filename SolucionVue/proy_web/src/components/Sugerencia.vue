<template>
    <v-layout align-start>
      <v-flex>
      <v-toolbar
      style="right: -144px"
       flat color="white">
        <v-toolbar-title class="flex text-center">Sintomas  </v-toolbar-title>
        <v-divider
        
        class="mx-4"
        inset  
        vertical ></v-divider>
        <v-toolbar-title class="flex text-center">Alergias  </v-toolbar-title>

        <v-spacer></v-spacer>
        <v-spacer></v-spacer>
       
        
      </v-toolbar>
      
      <v-spacer></v-spacer>
      <v-spacer></v-spacer>
      <v-spacer></v-spacer>

      <v-row style=" margin: 30px 39px;">
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
          > <option v-for="medicamento in medicamentos" :key="medicamento.idMedicamento" :value="medicamento.compuestoQuimico" >
             {{medicamento.compuestoQuimico}}
        </option></select> 
    
        <!-- <p>Seleccion {{select}}</p> -->
        </v-col>
        
       
    </v-row>
    
    <v-spacer></v-spacer>
    <v-spacer></v-spacer>

    <v-row >
        <v-col>
        <v-dialog v-model="dialog" max-wiidth="500px">
          <template v-slot:activator="{ on, attrs }">
          <v-btn 
          style="    right: -414px; top: 10px;"
          color="teal lighten-3" dark class="mb-2" v-bind="attrs" v-on="on" @click="Comparacion">
            Consultar medicamentos
          </v-btn>

          </template>
        </v-dialog>
      </v-col>
    </v-row>

    <v-data-table
          :headers="headers"
          :items="medicamentosComparacion"
          :search="search"
          class="elevation-1"
          v-show="tabla"
        >
       

        <template v-slot:[`item.actions`]="{ item }">
          
          <template>
            <v-container
              class="px-0"
              fluid
            >
              <input
                type="checkbox"

                @change="MedicamentoCheckbox(item,$event)"

              />
            </v-container>
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
        <v-spacer></v-spacer>
        <v-spacer></v-spacer>
        
        </v-data-table>
        <v-row v-show="tabla">
        <v-col>
        <v-dialog v-model="dialog" max-wiidth="500px" >
          <template v-slot:activator="{ on, attrs }">
          <v-btn 
          style="    right: -414px; top: 10px;"
          color="teal lighten-3" dark class="mb-2" v-bind="attrs" v-on="on" @click="GuardarSugerencia">
            Guardar Sugerencia
          </v-btn>

          </template></v-dialog></v-col>
    </v-row>
<v-spacer></v-spacer>

   
   
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
        tabla:false,
        medicamentos:[],
        medicamentosComparacion:[],
        medicamentoSugerencia:[],
        
        headers: [
          { text: "Nombre Medicamento", value: "nombre"},
          { text: "Compuesto Quimico", value: "compuestoQuimico" },
          { text: "Opciones", value: "actions", sortable: false },
          
        ],
        search: '',
        editedIndex: -1,
        idMedicamento_Sintoma:0,
          nombre:"",
          compuestoQuimico:"",
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
     // this.Comparacion();
      this.ListarSintomas();
    },
    methods: {
      Listar() {
        let me = this;
        let header={"Authorization" : "Bearer " + this.$store.state.token};
        let configuracion= {headers : header};
        axios.get("/api/Medicamentos/ListarSelect",configuracion)
          .then(function (response) {
            console.log(response);
            me.medicamentos = response.data;
            
            
          })
          .catch(function (error) {
            console.log(error);
          });
      },
      MedicamentoCheckbox(item,event){
        if(event.target.checked){
          if(this.medicamentoSugerencia.length<3){
           this.medicamentoSugerencia.push(item)
          }else{
            console.log("No se puede agregar mas de tres elementos")
          }
        }else{
          this.medicamentoSugerencia.find(i => i.idMedicamento== item.idMedicamento)
          let i=0;
          for( i=0 ; i<this.medicamentoSugerencia.length; i++){
            if(this.medicamentoSugerencia[i].idMedicamento==item.idMedicamento)
              break
          }
          this.medicamentoSugerencia.splice(item,1)
 
 
        }
        console.log(this.medicamentoSugerencia)

      },
      Comparacion() {
        this.tabla=true;
        let me = this;
        let header={"Authorization" : "Bearer " + this.$store.state.token};
        let configuracion= {headers : header};
        axios.get("/api/Medicamentos/ListarMedicamentoCore/"+this.select+":"+this.select2,configuracion)
          .then(function (response) {
            //console.log(response);
            me.medicamentosComparacion = response.data;
            
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
      
      ListarSintomas() {
        let me = this;
        let header={"Authorization" : "Bearer " + this.$store.state.token};
        let configuracion= {headers : header};
        axios.get("/api/Sintomas/ListarSelect",configuracion)
          .then(function (response) {
            console.log(response);
            me.sintomas = response.data;
          })
          .catch(function (error) {
            console.log(error);
          });
      },
      GuardarSugerencia() {
         let i=0;
          for(i=0;i<this.medicamentoSugerencia.length; i++){
            let me=this;
            console.log(this);
            
             axios.post('/api/SugerenciaMedicinas/Crear',{
              'idMedicamento_FK': this.medicamentoSugerencia[i].idMedicamento,
    
            }, {headers: {
              "Authorization":"Bearer " + this.$store.state.token
              
            }}).then(function(response){
              me.close();
              me.Listar();
              me.limpiar();
              
            }).catch(function (error){
              console.log(error);
            });
          }
            
        },
      
  
      limpiar(){
        this.idMedicamento_Sintoma="",
        this.nombre="",
        this.lugarSintoma= "",
        this.tipoMalestar="",
        this.editedIndex =-1;
      },
      
      

    },
  };
  </script>