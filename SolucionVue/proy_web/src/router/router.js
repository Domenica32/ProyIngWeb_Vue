import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Usuarios from '../components/Usuarios.vue'
import Rol from '../components/Rol.vue'
import LoginUsuarios from '../components/LoginUsuarios.vue'
import Medicamentos from '../components/Medicamentos.vue'
import Sintomas from '../components/Sintomas.vue'
import MedicamentoSintoma from '../components/MedicamentoSintoma.vue'
import Sugerencia from '../components/Sugerencia.vue'

import store from '../store/store'

Vue.use(VueRouter)

var router = new VueRouter(
  {
      mode: 'history',
      base: process.env.BASE_URL,
      routes : [
        {
          path: '/',
          name: 'home',
          component: HomeView,
          meta: { //prpiedad meta
            administrador:true,//tipo de usuario que tiene acceso a ese path
            usuarioNormal:true
          }
        },
        {
          path: '/about',
          name: 'about',
          // route level code-splitting
          // this generates a separate chunk (about.[hash].js) for this route
          // which is lazy-loaded when the route is visited.
          component: () => import(/* webpackChunkName: "about" */ '../views/AboutView.vue')
        },
        {
          path: '/Medicamentos',
          name: 'Medicamentos',
          component: Medicamentos,
          meta: { //prpiedad meta
            administrador:true,//tipo de usuario que tiene acceso a ese path
          }
        },
        {
          path: '/Sintomas',
          name: 'Sintomas',
          component: Sintomas,
          meta: { //prpiedad meta
            administrador:true,//tipo de usuario que tiene acceso a ese path
          }
        },
        {
          path: '/MedicamentoSintoma',
          name: 'MedicamentoSintoma',
          component: MedicamentoSintoma,
          meta: { //prpiedad meta
            administrador:true,//tipo de usuario que tiene acceso a ese path
          }
        },
        {
          path: '/Usuarios',
          name: 'Usuaurios',
          component: Usuarios,
          meta: {
            administrador:true
            
          }
        },
        {
          path: '/roles',
          name: 'roles',
          component: Rol,
          meta: {
            administrador:true
          }
        },
        {
          path: '/sugerencia',
          name: 'sugerencia',
          component: Sugerencia,
          meta: {
            usuarioNormal:true
          }
        },
        {
          path: '/loginUsuarios',
          name: 'loginUsuarios',
          component: LoginUsuarios,
          meta:{
            libre:true//todos pueden ver lo que esta en la ruta login
          }
        }
      
      ]
    
  })
  //validar las rutas con el objeto router
  router.beforeEach((to, from, next) => {
      //console.log("usuario>: ",store.state.usuario.idRol)
    if(to.matched.some(record => record.meta.libre)){
      next()
    }else if (store.state.usuario && store.state.usuario.idRol == 1  ){ //  && store.state.usuario.Rol == 1 que exista un usuario es decir que sea diferente de null y que el rol sea igual a dministrador
      if(to.matched.some(record => record.meta.administrador)){ // que la ruta a la que deseo acceder tiene la meta administrador le dejo pasar
        next()
      }
    }else if (store.state.usuario && store.state.usuario.idRol == 2   ){//&& store.state.usuario.Rol == 2 
      if(to.matched.some(record => record.meta.usuarioNormal)){
        next()
      }
    }else {//llamar a la ruta login
      next({
        name: 'loginUsuarios'
      })
    }

  }) 
//exportar el objeto router
  export default router




