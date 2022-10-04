<template>
  <v-layout align-start>
    <v-flex>
    <v-toolbar flat color="white">
      <v-toolbar-title>Usuarios</v-toolbar-title>
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
        Nuevo Usuario
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
                  <v-text-field v-model="idRolUsuarios_FK" label="Rol Usuario"></v-text-field>
                </v-flex>
                <v-flex  xs12 sm12 md12>
                  <v-text-field v-model="NombreUsuario" label="Nombre Uusuario"></v-text-field>
                </v-flex>
                <v-flex  xs12 sm12 md12>
                  <v-text-field v-model="ApellidoUsuario" label="Apellido Usuario"></v-text-field>
                </v-flex>
                <v-flex  xs12 sm12 md12>
                  <v-text-field v-model="EmailUsuario" label="Email Usuario"></v-text-field>
                </v-flex>
                <v-flex  xs12 sm12 md12>
                  <v-text-field v-model="PasswordUsuario_hash" label="Password"></v-text-field>
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
            <v-btn color="blue darke-1" text  @click="close">Cancel</v-btn>
            <v-btn color="blue darke-1" text  @click="save">Save</v-btn>

          </v-card-actions>
        </v-card> 
      </v-dialog>
     <!--<v-dialog v-model="dialogDelete" max-width="500px">
        <v-card>
          <v-card-title class="text-h5">Estas seguro que deseas eliminarlo?</v-card-title>
          <v-card-action>
            <v-spacer></v-spacer>
            <v-btn color="blue darke-1" text  @click="closeDelete">Cancel</v-btn>
            <v-btn color="blue darke-1" text  @click="deleteItemConfirm">Ok</v-btn>
            <v-spacer></v-spacer>
          </v-card-action>
        </v-card>
      </v-dialog>--> 
    </v-toolbar>

<v-data-table
        :headers="headers"
        :items="usuarios"
        :search="search"
        class="elevation-1"
      >
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
      usuarios: [],
      dialog: false,
      headers: [
        
        { text: "Rol del Usuario", value: "idRolUsuarios_FK", sortable: false, align:"start"},
        { text: "Nombre", value: "nombreUsuario" },
        { text: "Apellido", value: "apellidoUsuario" },
        { text: "Email", value: "emailUsuario" },
        { text: "Password", value: "passwordUsuario_hash"},
        { text: "Opciones", value: "actions", sortable: false },
      ],
      search: '',
      desserts: [],
      editedIndex: -1,
      editedItem: {
        idRolUsuarios_FK:0,
        NombreUsuario: "",
        ApellidoUsuario:"",
        EmailUsuario: "",
        PasswordUsuario_hash:""
        
      },
        idUsuarios:0,
        idRolUsuarios_FK:0,
        NombreUsuario: "",
        ApellidoUsuario:"",
        EmailUsuario: "",
        PasswordUsuario_hash:"",
        valida:0,
        validaMensaje:[],
    };
  },
  computed: {
    formTitle() {
      return this.editedIndex === -1 ? 'Nuevo Usuario' : 'Editar Usuario';
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
  },
  methods: {
    Listar() {
      let me = this;
      axios.get("/api/UsuariosRols/Listar")
        .then(function (response) {
          //console.log(response);
          me.usuarios = response.data;
        })
        .catch(function (error) {
          console.log(error);
        });
    },
    initialize() {
    this.desserts = [];
    },

    editItem(item) {
      //this.editedIndex = this.desserts.indexOf(item);
      //this.editedItem = Object.assign({}, item);
      //console.log(this.editedItem);
      this.idUsuarios = item.idUsuarios;
      this.idRolUsuarios_FK = item.idRolUsuarios_FK;
      this.NombreUsuario = item.nombreUsuario;
      this.ApellidoUsuario = item.apellidoUsuario;
      this.EmailUsuario = item.emailUsuario;
      this.PasswordUsuario_hash = item.passwordUsuario_hash;
      this.editedIndex=1;
      this.dialog = true;
    },

    deleteItem(item) {
      this.editedIndex = this.desserts.indexOf(item);
      this.editedItem = Object.assign({}, item);
      this.dialogDelete = true;
    },

    deleteItemConfirm() {
      this.desserts.splice(this.editedIndex, 1);
      this.closeDelete();
    },

    close() {
      this.dialog = false;
    },

    closeDelete() {
      this.dialogDelete = false;
      this.$nextTick(() => {
        this.editedItem = Object.assign({}, this.defaultItem);
        this.editedIndex = -1;
      });
    },
    limpiar(){
      this.idRolUsuarios_FK="";
      this.NombreUsuario="";
      this.ApellidoUsuario="";
      this.EmailUsuario="";
      this.PasswordUsuario_hash="";

    },
    save() {
      
     // if (this.validar()){
      //  return; 
     // }

      if (this.editedIndex > -1) {
        //Codigo para editar
        let me=this;
        console.log(this);
        axios.put('/api/UsuariosRols/Actualizar',{
          'idUsuarios':parseInt(me.idUsuarios),
          'idRolUsuarios_FK':parseInt(me.idRolUsuarios_FK),
          'NombreUsuario': me.NombreUsuario,
          'ApellidoUsuario': me.ApellidoUsuario,
          'EmailUsuario': me.EmailUsuario,
          'PasswordUsuario_hash':me.PasswordUsuario_hash,
          'PasswordUsuario_desencrip':me.PasswordUsuario_hash

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
        axios.post('/api/UsuariosRols/Crear',{
          'idRolUsuarios_FK':parseInt(me.idRolUsuarios_FK),
          'NombreUsuario': me.NombreUsuario,
          'ApellidoUsuario': me.ApellidoUsuario,
          'EmailUsuario': me.EmailUsuario,
          'PasswordUsuario_hash':me.PasswordUsuario_hash,
          'PasswordUsuario_desencrip':me.PasswordUsuario_hash

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
      this.valida+0;
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