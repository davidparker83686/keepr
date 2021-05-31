<template>
  <div>
    <button type="button" class="btn">
      <router-link style="color: inherit;" :to="{name: 'Profile', params: {id: vault.creatorId}}">
        <div class="row justify-content-between">
          {{ vault.name }}
          {{ vault.img }}
        </div>
      </router-link>
    </button>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { AppState } from '../AppState'
// import { vaultsService } from '../services/vaultsService'
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
      vault: computed(() => AppState.vaults)
    })
    return {
      state
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
</style>
