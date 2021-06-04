<template>
  <div class="card my-2" style="max-width:20vw;">
    <router-link style="color: inherit;" :to="{name: 'Vaults', params: {id: vault.id}}" @click="activeVault(vault.id)">
      <div class="row justify-content-between">
        <h4 class="position-absolute m-2">
          {{ vault.name }}
        </h4>
        <img v-if="vault.img" class="img-fluid rounded " :src="vault.img " alt="Vault Image">
      </div>
    </router-link>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
import { vaultsService } from '../services/VaultsService'
// import { logger } from '../utils/Logger'
// import Notification from '../utils/Notification'
export default {
  name: 'Vault',
  props: {
    vault: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      user: computed(() => AppState.user),
      account: computed(() => AppState.account),
      vaults: computed(() => AppState.vaults)
    })
    return {
      state,
      async activeVault(id) {
        try {
          await vaultsService.activeVault(id)
        } catch (error) {
          console.error(error)
        }
      }
    }
  }
}
</script>

<style scoped>
/* .dropdown-menu {
  user-select: none;
  display: block;
  transform: scale(0);
  transition: all 0.15s linear;
}
.dropdown-menu.show {
  transform: scale(1);
}
.hoverable {
  cursor: pointer;
}
a:hover {
  text-decoration: none;
}
.nav-link{
  text-transform: uppercase;
}
.nav-item .nav-link.router-link-exact-active{
  color: var(--primary);
}
.available {
  color: var(--success)
}
.not-available {
  color: var(--danger)
}
.lightgrey{
  background-color: rgba(236, 236, 236, 0.315);
}
@media screen and (min-width:760px){
  .userbuttons{
justify-content: space-around;
  }
} */
/* .cardbg{
  :background-image: vault.img;
} */
img{
  width: 100%;
}
</style>
