<template>
  <div class="profile container-fluid">
    <!-- Profile info -->
    <div class="row">
      <div class="col">
        {{ state.activeAccount.picture }}
      </div>
      <div class="col">
        {{ state.activeAccount.name }}
        <span>Vaults: </span>
        <span>Keepss: </span>
      </div>
    </div>
    <!-- Vaults -->
    <div class="row">
      <div class="col-12">
        <span>Vaults</span>
        <button type="button"
                class="btn btn-none text-success"
                data-toggle="modal"
                title="Create Vault"
                aria="Create Vault"
                data-target="#newVaultModal"
        >
          <i class="fas fa-plus"></i>
        </button>
      </div>
      <div class="col">
        <Vault v-for="vault in state.vaults" :key="vault.id" :vault="vault" />
      </div>
    </div>
    <!-- Keeps -->
    <div class="row">
      <div class="col-12">
        <span>Keeps</span>
        <button type="button"
                class="btn btn-none text-success"
                title="Create Keep"
                aria="Create Vault"
                data-toggle="modal"
                data-target="#newKeepModal"
        >
          <i class="fas fa-plus"></i>
        </button>
      </div>
      <div class="col">
        <Keep v-for="keep in state.keeps" :key="keep.id" :keep="keep" />
      </div>
    </div>
    <new-keep-modal />
    <new-vault-modal />
  </div>
</template>

<script>
import { computed, onMounted, reactive } from 'vue'
import { AppState } from '../AppState'
import { useRoute } from 'vue-router'
import { accountService } from '../services/AccountService'
import { keepsService } from '../services/KeepsService'
import { vaultsService } from '../services/VaultsService'
import NewKeepModal from '../components/NewKeepModal.vue'
export default {
  components: { NewKeepModal },
  name: 'Profile',
  setup() {
    const route = useRoute()
    const state = reactive({
      keep: computed(() => AppState.keeps),
      vault: computed(() => AppState.vaults),
      activeAccount: computed(() => AppState.activeAccount),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user)
    })
    // watch(() => state.loading, () => {
    //   accountService.getActive(route.params.id)
    // })
    onMounted(async() => {
      try {
        if (!state.loading) {
          await accountService.getActive(route.params.id)
        }
        await keepsService.getKeepsByUserId(route.params.id)
        await vaultsService.getVaultsByUserId(route.params.id)
      } catch (error) {
        console.error(error)
      }
    })

    return {
      state,
      route

    }
  }
}
</script>

<style scoped>
img {
  max-width: 100px;
}
</style>
