<template>
  <nav class="navbar navbar-expand-md rounded-2 mx-auto my-2 w-100 header-shell">
    <div class="container-fluid align-items-stretch">
      <router-link
        class="navbar-brand d-flex align-items-center"
        :to="{ name: APP_ROUTE_NAMES.HOME }"
      >
        <img
          src="@/assets/michal.jpg"
          alt="Logo"
          class="d-inline-block align-text-top me-2"
          height="30"
        />
        Eccommerce
      </router-link>
      <button
        class="navbar-toggler"
        type="button"
        data-bs-toggle="collapse"
        data-bs-target="#navbarSupportedContent"
        aria-controls="navbarSupportedContent"
        aria-expanded="false"
        aria-label="Toggle navigation"
      >
        <span class="navbar-toggler-icon"></span>
      </button>
      <div class="collapse navbar-collapse header-collapse" id="navbarSupportedContent">
        <ul class="navbar-nav me-auto mb-2 mb-lg-0 align-items-md-center">
          <li class="nav-item">
            <router-link class="nav-link" aria-current="page" :to="{ name: APP_ROUTE_NAMES.HOME }"
              >Home</router-link
            >
          </li>
          <li class="nav-item">
            <router-link class="nav-link" :to="{ name: APP_ROUTE_NAMES.ORDER_LIST }"
              >Orders</router-link
            >
          </li>
          <li class="nav-item dropdown">
            <a
              class="nav-link dropdown-toggle"
              href="#"
              role="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              Admin
            </a>
            <ul class="dropdown-menu">
              <li>
                <router-link class="dropdown-item" :to="{ name: APP_ROUTE_NAMES.MENU_ITEM_LIST }"
                  >Menu Items</router-link
                >
              </li>
              <li>
                <router-link
                  class="dropdown-item"
                  :to="{ name: APP_ROUTE_NAMES.MANAGE_ORDER_ADMIN }"
                  >Order Management</router-link
                >
              </li>
            </ul>
          </li>
        </ul>

        <ul class="navbar-nav ms-auto mb-2 mb-lg-0 align-items-md-center header-actions">
          <li class="nav-item d-flex align-items-center">
            <span class="nav-link mb-0">Cart</span>
          </li>
          <li class="nav-item">
            <router-link
              class="nav-link position-relative cart-link"
              :to="{ name: APP_ROUTE_NAMES.CART }"
            >
              <i class="bi bi-cart3"></i>
              <span
                class="position-absolute top-0 start-100 translate-middle badge rounded-pill bg-danger"
                >0</span
              >
            </router-link>
          </li>
          <li class="nav-item">
            <router-link class="nav-link" :to="{ name: APP_ROUTE_NAMES.SIGN_IN }"
              >Sign In</router-link
            >
          </li>
          <li class="nav-item">
            <router-link class="nav-link" :to="{ name: APP_ROUTE_NAMES.SIGN_UP }"
              >Sign Up</router-link
            >
          </li>
          <li class="nav-item">
            <button class="nav-link btn btn-link text-decoration-none border-0 p-0" type="button">
              Logout
            </button>
          </li>
          <li class="nav-item dropdown">
            <a
              class="nav-link dropdown-toggle"
              href="#"
              role="button"
              data-bs-toggle="dropdown"
              aria-expanded="false"
            >
              <i class="bi bi-laptop"></i>
            </a>
            <ul class="dropdown-menu dropdown-menu-end">
              <li>
                <button class="dropdown-item" type="button" @click="themeStore.setTheme('light')">
                  <i class="bi bi-sun"></i> Light
                </button>
              </li>
              <li>
                <button class="dropdown-item" type="button" @click="themeStore.setTheme('dark')">
                  <i class="bi bi-moon"></i> Dark
                </button>
              </li>
            </ul>
          </li>
        </ul>
      </div>
    </div>
  </nav>
</template>

<script setup>
import APP_ROUTE_NAMES from '@/constans/routeNames.js'
import { useThemeStore } from '@/stores/themeStore.js'

const themeStore = useThemeStore()
</script>
