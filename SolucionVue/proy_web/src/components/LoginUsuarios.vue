<template>
    <v-container fill-height fluid> 
        <v-row align="center" justify="center">
        <v-flex xs12 sm8 md6 lg5 x14 >
            <v-card>
                <v-toolbar dark color="teal lighten-3">
                    <v-toolbar-title>Acceso al sistema  </v-toolbar-title>
                </v-toolbar>
                <v-card-text>
                    <v-text-field v-model="EmailUsuario" autofocus color="accent" label="Email" required></v-text-field>
                    <v-text-field v-model="PasswordUsuario" type="password" color="accent" label="Password" required></v-text-field>
                    <v-flex class="red--text" v-if="error">
                        {{error}}
                    </v-flex>
                </v-card-text>
                <v-card-actions class="px-3 pb-3">
                    <v-flex text-xs-rigth>
                        <v-btn @click="ingresar" color="#808080">Ingresar</v-btn>
                    </v-flex>

                </v-card-actions>
            </v-card>
        </v-flex>
         </v-row>
     </v-container>

</template>

<script>
import axios from 'axios'
export default{
    data()
    {
        return{
            EmailUsuario:'',
            PasswordUsuario:'',
            error:null
        }
    },
    methods :{
        ingresar(){
            this.error=null;
            axios.post('api/UsuariosRols/Login',{EmailUsuario: this.EmailUsuario, PasswordUsuario:this.PasswordUsuario})
            .then(respuesta =>{
                return respuesta.data
            })
            .then(data =>{
                this.$store.dispatch("guardarToken", data.token)//activamos la accion guardarToken
                this.$router.push({name:'home'})
            })
            .catch(err => {
                //console.log(err.response);
                if(err.response.status==400){
                    this.error="No es un email valido";
                }else if (err.response.status==404){
                    this.error="No existe el usuario o sus datos son incorrectos";
                }else{
                    this.error="Ocurrio un error";

                }
                console.log(err)
            })
        }
    }

}
</script>