<template>
  <div class="modal minview"
       id="copyKeepDetailsModal"
       tabindex="-1"
       role="dialog"
       aria-labelledby="exampleModalLabel"
       aria-hidden="true"
       style="min-width:80vw;"
  >
    <!-- :id="'keepDetailsModal'+ keepProp.id" -->
    <div class="modal-dialog" role="document">
      <div class=" modal-content">
        <!-- ________________________________________________________________________________________________________ -->

        <!-- ________________________________________________________________________________________________________ -->
        <div class="row m-0">
          <div class="col-6 d-inline p-2 m-0">
            <img :src="state.activeKeep.img " class="img-fluid" alt="Keep Image">
          </div>

          <div class="col-6  d-inline">
            <div class="row justify-content-end">
              <button type="button" class="btn modal-header txtlft btn-none text-danger shadow-none border-bottom-0  " title="close" aria="close" data-dismiss="modal">
                <i class="fas fa-times"></i>
              </button>
            </div>

            <div class="row justify-content-center">
              <h3 class="">
                {{ state.activeKeep.name }}
              </h3>
            </div>
            <div class="row justify-content-center">
              {{ state.activeKeep.description }}
            </div>
            <div class="row justify-content-center my-3">
              {{ state.activeKeep.views }}<i class="far fa-eye"></i>
              {{ state.activeKeep.keeps }} <i class="fab fa-kaggle"></i>
              {{ state.activeKeep.shares }}<i class="fas fa-share"></i>
            </div>
            <div class="row justify-content-around">
              <div class="col-5 text-left">
                <div class="dropdown  d-flex justify-content-center mb-2">
                  <button class="btn drop btn-outline-success dropdown-togglen w-100 mt-1 mb-2 mx-2"
                          type="button"
                          id="dropdownMenuButton"
                          data-toggle="dropdown"
                          aria-haspopup="true"
                          aria-expanded="false"
                          title="Add To Vault"
                  >
                    AddToVault
                  </button>
                  <div class="dropdown-menu clickable" aria-labelledby="dropdownMenuButton">
                    <div class="dropdown-item clickable" href="#" v-for="vault in state.vaults" :key="vault.id" @click="createVaultKeep(vault.id)">
                      <span>
                        hi
                      </span>
                      {{ vault.name }}
                    </div>
                  </div>
                </div>
              </div>

              <div class="col-7 text-right clickable">
                <!-- ________________________________________________________________________________________________________ -->

                <div v-if="state.activeKeep.creatorId">
                  <!-- <div v-if="state.activeKeep.creatorId != state.account.id"> -->
                  <router-link style="color: inherit;" :to="{name: 'Profiles', params: {id: state.activeKeep.creatorId}} ">
                    <img class="rounded-circle minipix inline" :src="state.activeKeep.creator.picture" alt="Creator Photo">
                    <span class="inline">{{ (state.activeKeep.creator.name.split('@')[0]) }}</span>
                  </router-link>
                </div>
                <!-- ________________________________________________________________________________________________________ -->
                <div v-if="state.activeKeep.creatorId == state.account.id">
                  <button type="button" class="btn btn-outline-danger" @click="deleteKeep(state.activeKeep.id)" data-dismiss="modal">
                    Delete Keep
                  </button>
                </div>
                <!-- ______________________________________________________________________________________________________ -->
              </div>
            </div>
          </div>
        </div>
      </div>
    </div>
  </div>
</template>

<script>
import { reactive, computed } from 'vue'
import { keepsService } from '../services/KeepsService'
// import { vaultsService } from '../services/VaultsService'
import Notification from '../utils/Notification'
// import $ from 'jquery'
import { AppState } from '../AppState'
// import { accountService } from '../services/AccountService'
import { logger } from '../utils/Logger'

export default {
  name: 'CopyKeepDetailsModal',
  props: {
    copyKeepDetailsModal: {
      type: Object,
      required: true
    // keepProp: {
    //   type: Object,
    //   required: true
    }
  },
  setup(props) {
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user),
      activeKeep: computed(() => AppState.activeKeep),
      // vaults: computed(() => AppState.vaults),
      vaults: computed(() => AppState.vaults)
    })
    // onMounted(async() => {
    //   try {
    //     await vaultsService.getVaultsByUserId(state.account.id)
    //   } catch (error) {
    //     logger.error(error)
    //   }
    // })
    return {
      state,
      async deleteKeep(id) {
        try {
          if (await Notification.confirmAction('Are you sure you want to delete this keep?', 'You won\'t be able to revert this.', '', 'Yes, Delete')) {
            await keepsService.deleteKeep(id)
            '#copyKeepDetailsModal'.modal('hide')
            Notification.toast('Successfully Deleted Keep', 'success')
          }
        } catch (error) {
          logger.error(error)
        }
      },
      async createVaultKeep(id) {
        try {
          await keepsService.createVaultKeep(id, state.activeKeep.id)
          state.activeKeep = {}
          '#copyKeepDetailsModal'.modal('hide')
        } catch (error) {
          logger.error(error)
        }
      }
      // async getVaultsByUserId(userInfo) {
      //   try {
      //     await vaultsService.getVaultsByUserId(userInfo)
      //   } catch (error) {
      //     logger.error(error)
      //   }
      // }

    }
  },
  components: {}
}
</script>

<style scoped>
.clickable{
cursor: pointer
}
/* .minview{
  width: 200px
} */
.txtlft{
    text-align: right;
}
.minipix{
  width: 50%
}
.inline{
    display: inline-block
}
</style>
