<template>
    <v-container fill-height fluid> 
        <v-row align="center" justify="center">
        <v-flex xs12 sm8 md6 lg5 x14 >
            <v-card>
                <v-toolbar dark color="teal lighten-3">
                     <v-toolbar-tittle>Acceso al sistema</v-toolbar-tittle>  
                </v-toolbar>
                <v-card-text>
                    <v-text-field v-model="EmailUsuarios" autofocus color="accent" label="Email" required></v-text-field>
                    <v-text-field v-model="PasswordUsuarios" type="password" color="accent" label="Password" required></v-text-field>

                </v-card-text>
                <v-card-actions class="px-3 pb-3">
                    <v-flex text-xs-rigth>
                        <v-btn color="#808080">Ingresar</v-btn>
                    </v-flex>

                </v-card-actions>
            </v-card>
        </v-flex>
         </v-row>
     </v-container>

</template>

<script>
export default{
    data()
    {
        return{
            EmailUsuarios:"",
            PasswordUsuarios:""
        }
    }

}
</script>