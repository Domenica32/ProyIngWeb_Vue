import Vue from 'vue'
import VueRouter from 'vue-router'
import HomeView from '../views/HomeView.vue'
import Usuarios from '../components/Usuarios.vue'
import Rol from '../components/Rol.vue'
import LoginUsuarios from '../components/LoginUsuarios.vue'


Vue.use(VueRouter)

const routes = [
  {
    path: '/',
    name: 'home',
    component: HomeView
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
    path: '/Usuarios',
    name: 'Usuaurios',
    component: Usuarios
  },
  {
    path: '/roles',
    name: 'roles',
    component: Rol
  },
  {
    path: '/loginUsuarios',
    name: 'loginUsuarios',
    component: LoginUsuarios
  }

]

const router = new VueRouter({
  mode: 'history',
  base: process.env.BASE_URL,
  routes
})

export default router
