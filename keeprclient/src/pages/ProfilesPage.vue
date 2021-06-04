<template>
  <div class="profilesPage container-fluid m-md-5">
    <!-- Profile info -->
    <div class="row">
      <div class="col-md-3 ">
        <img :src="state.activeProfile.picture" class=" imgsize" alt="Profile Pic">
        <!-- {{ state.account.picture }} -->
      </div>
      <div class="col-md-8 p-0">
        <span class="biggest-font-size" v-if="state.account">
          {{ state.activeProfile.name }}
        </span>
        <br>
        <span class="smallest-font-size">Vaults:{{ getUserVaults() }} </span>
        <br>
        <span class="smallest-font-size">Keeps:{{ getUserKeeps() }} </span>
      </div>
    </div>
    <!-- Vaults -->
    <div class="row">
      <div class="col-12 mt-md-5">
        <span class="middle-font-size">Vaults</span>
        <button type="button"
                class="btn btn-none shadow-none text-success"
                data-toggle="modal"
                title="Create Vault"
                aria="Create Vault"
                data-target="#newVaultModal"
        >
          <i class="fas fa-plus"></i>
        </button>
      </div>
      <div class="col-12 d-flex flex-wrap justify-content-between">
        <Vault v-for="vault in state.accountVaults" :key="vault.id" :vault="vault" />
      </div>
    </div>
    <!-- Keeps -->
    <div class="row">
      <div class="col-12">
        <span class="middle-font-size">Keeps</span>
        <button type="button"
                class="btn btn-none shadow-none text-success"
                title="Create Keep"
                aria="Create Vault"
                data-toggle="modal"
                data-target="#newKeepModal"
        >
          <i class="fas fa-plus"></i>
        </button>
      </div>
      <div class="col-12">
        <div class="card-columns">
          <Keep v-for="keep in state.accountKeeps" :key="keep.id" :keep="keep" />
        </div>
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
  name: 'Profiles',
  setup() {
    const route = useRoute()
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      vaults: computed(() => AppState.vaults),
      activeProfile: computed(() => AppState.activeProfile),
      accountVaults: computed(() => AppState.accountVaults),
      accountKeeps: computed(() => AppState.accountKeeps),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user)
    })
    // watch(() => state.loading, () => {
    //   accountService.getActive(route.params.id)
    // })
    onMounted(async() => {
      try {
        // if (!state.loading) {
        //   await accountService.getActive(route.params.id)
        // }
        await keepsService.getKeepsByProfile(route.params.id)
        await accountService.makeActiveProfile(route.params.id)
        await keepsService.getKeepsByProfile(route.params.id)
        await vaultsService.getVaultsByProfile(route.params.id, state.account.id)
      } catch (error) {
        console.error(error)
      }
    })

    return {
      state,
      route,
      getUserKeeps() {
        const totalKeeps = state.accountKeeps.length
        return totalKeeps
      },
      getUserVaults() {
        const totalVaults = state.accountVaults.length
        return totalVaults
      }

    }
  }
}
</script>

<style scoped>
/* img {
  max-width: 100px;
} */
.imgsize {
  min-width: 15rem;
  height: auto;
}
.biggest-font-size{
  font-size: 3rem;
}
.middle-font-size{
font-size: 2rem;
}
.smallest-font-size{
  font-size: 2rem;
}
</style>
