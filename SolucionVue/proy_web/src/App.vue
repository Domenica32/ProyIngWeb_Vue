<template>
  <v-app id="inspire">
    <v-navigation-drawer

      color="#808080"
      dark
      v-model="drawer"
      v-if="logueado"
      app
    >
    
   
      <template v-if="esAdministrador ">
        <v-list dense>
        <v-list-item
          v-for="item in itemsAdmin"
          :key="item.title"
          :to ="item.to"
          link
        >
          <v-list-item-icon>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
      </template>
      <template v-if="esUsuarioNormal ">
        <v-list dense>
        <v-list-item
          v-for="item in itemsNormal"
          :key="item.title"
          :to ="item.to"
          link
        >
          <v-list-item-icon>
            <v-icon>{{ item.icon }}</v-icon>
          </v-list-item-icon>

          <v-list-item-content>
            <v-list-item-title>{{ item.title }}</v-list-item-title>
          </v-list-item-content>
        </v-list-item>
      </v-list>
      </template>
      

      <template v-slot:append>
        <div class="pa-2">
          <v-btn @click="salir" block>
            Logout
          </v-btn>
        </div>
      </template>
    </v-navigation-drawer>

    <v-app-bar
      app
      color="teal lighten-3"
      dark
      
      scroll-target="#scrolling-techniques-4"
    >
      <v-app-bar-nav-icon @click="drawer=!drawer"></v-app-bar-nav-icon>

      <v-toolbar-title> Be Safe!</v-toolbar-title>

      <v-spacer></v-spacer>

      <v-btn icon>
        <v-icon>mdi-magnify</v-icon>
      </v-btn>

      <v-btn icon>
        <v-icon>mdi-heart</v-icon>
      </v-btn>

      <v-btn icon>
        <v-icon>mdi-dots-vertical</v-icon>
      </v-btn>
    </v-app-bar>

    <v-main>
      <router-view></router-view>
    </v-main>
  </v-app>
</template>

<script>
  export default {
    data: () => ({ 
      drawer: null,
      itemsAdmin: [
        
          {title: 'Inicio', icon:'mdi-ray-start-vertex-end', to:'/' },
          { title: 'Usuarios', icon: 'mdi-account-plus', to:'/Usuarios'},
          { title: 'Roles', icon: 'mdi-shield-account', to:'/roles'},
          { title: 'Medicamentos', icon: 'mdi-pill-multiple', to:'/Medicamentos'},


        ], 
      itemsNormal:[
      {title: 'Inicio', icon:'mdi-ray-start-vertex-end', to:'/' },

      ],
    
    }),
    computed:{
      logueado(){
        return this.$store.state.usuario;
      },
      esAdministrador(){
        console.log('rol:',this.$store.state.usuario)
        return this.$store.state.usuario && this.$store.state.usuario.idRol == 1 ;
      },
      esUsuarioNormal(){
        return this.$store.state.usuario && this.$store.state.usuario.idRol == 2  ;

      }
    },created(){
      this.$store.dispatch("autoLogin");//llamamos a la accion autoLogin del store
    },
    methods:{
        salir(){
          this.$store.dispatch("salir");
        }
    }
  }
</script>