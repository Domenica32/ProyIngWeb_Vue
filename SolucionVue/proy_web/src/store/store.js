import Vue from 'vue'
import Vuex from 'vuex'
import decode from 'jwt-decode'
import router from '../router/router'
Vue.use(Vuex)

export default new Vuex.Store({
  state: {
    token: null,
    usuario: null,
  },
  getters: {
  },
  mutations: {
    setToken(state,token){
      state.token=token
    },
    setUsuario(state,usuario){
      state.usuario=usuario
    }
  },
  actions: {
    guardarToken({commit},token){//guardar al token
      commit("setToken",token)
      commit("setUsuario", decode(token))
      localStorage.setItem("token",token)
      console.log(token)
    },
    autoLogin({commit}){//verificar si el usuario ya ingreso sus credenciales correctas y se ha generado un token
        let token = localStorage.getItem("token")
        if(token){
          commit("setToken",token)
          commit("setUsuario", decode(token))          
        }
        router.push({name:'home'})
    },
    salir({commit}){//Eliminar el token que se encuentr registrado y si el usuario desea volver a ingresar al sistema debe ingresar con sus credenciales nuevamente
      commit("setToken",null)
      commit("setUsuario", null)
      localStorage.removeItem("token")
      router.push({name: 'loginUsuarios'})      
    }


  },
  modules: {
  }
})
