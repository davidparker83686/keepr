<template>
  <div class="container">
    <div class="modal "
         id="keepDetailsModal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="exampleModalLabel"
         aria-hidden="true"
    >
      <!-- :id="'keepDetailsModal'+ keepProp.id" -->
      <div class="modal-dialog" role="document">
        <div class=" modal-content">
          <!-- ________________________________________________________________________________________________________ -->
          <div class="row justify-content-center">
            <div class="col-10"></div>
            <div class="col-11 p-0 m-1 text-right">
              <button type="button" class="btn modal-header txtlft btn-none text-danger shadow-none border-none p-0 " title="close" aria="close" data-dismiss="modal">
                <i class="fas fa-times"></i>
              </button>
            </div>
          </div>
          <!-- ________________________________________________________________________________________________________ -->
          <div class="row m-0">
            <div class="col-6 bg-danger d-inline">
              <span>apple</span>
            </div>

            <div class="col-6 bg-info d-inline">
              <span>banana</span>
              <div class="row">
              <!-- <button type="button" class="btn modal-header btn-none text-danger" title="close" aria="close" data-dismiss="modal">
                <i class="fas fa-times"></i>
              </button> -->
              </div>

              <div class="row justify-content-center">
                {{ state.keeps.name }}
              </div>
              <div class="row justify-content-center">
                {{ state.keeps.description }}
              </div>
              <div class="row justify-content-center">
                {{ state.keeps.views }}<i class="far fa-eye"></i>
                {{ state.keeps.keeps }} <i class="fab fa-kaggle"></i>
                {{ state.keeps.shares }}<i class="fas fa-share"></i>
              </div>
              <div class="row justify-content-around">
                <!-- <div class="col-5 text-left">
                <div class="dropdown  d-flex justify-content-center mb-2">
                  <button class="btn drop btn-outline-success dropdown-togglen w-100 mt-1 mb-2 mx-2"
                          type="button"
                          id="dropdownMenuButton"
                          data-toggle="dropdown"
                          aria-haspopup="true"
                          aria-expanded="false"
                          title="Add To Vault"
                  >
                    Add To Vault
                  </button>
                  <div class="dropdown-menu clickable" aria-labelledby="dropdownMenuButton">
                    <div class="dropdown-item clickable" href="#" v-for="vault in state.vualt" :key="vault.id" @click="addToVault(vault.id)">
                      {{ vault.title }}
                    </div>
                  </div>
                </div>
              </div> -->

                <div class="col-5 text-right clickable">
                  <div v-if="state.keeps.creatorId != state.account.id">
                    <router-link style="color: inherit;" :to="{name: 'Profile', params: {id: state.keeps.creatorId}}">
                    <!-- {{ state.keeps.creator.picture }} -->
                    <!-- {{ state.keeps.creator.name }} -->
                    </router-link>
                  </div>
                  <div v-if="state.keeps.creatorId == state.account.id">
                    <button type="button" class="btn btn-outline-danger" @click="deleteKeep(keep.id)">
                      Delete Keep
                    </button>
                  </div>
                </div>
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
import $ from 'jquery'
import { AppState } from '../AppState'
// import { accountService } from '../services/AccountService'
import { logger } from '../utils/Logger'

export default {
  name: 'KeepDetailsModal',
  props: {
    // keepDetailsModal: {
    //   type: Object,
    //   required: true
    // },
    keepProp: {
      type: Object,
      required: true
    }
  },
  setup(props) {
    const state = reactive({
      keeps: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user)
    })
    return {
      state,
      async deleteKeep(id) {
        try {
          if (await Notification.confirmAction('Are you sure you want to delete this keep?', 'You won\'t be able to revert this.', '', 'Yes, Delete')) {
            await keepsService.deleteKeep(id)
            Notification.toast('Successfully Deleted Keep', 'success')
          }
        } catch (error) {
          logger.error(error)
        }
      },
      async addToVault() {
        try {
          await keepsService.addToVault(state.newKeep)
          state.newKeep = {}
          $('#keepDetailsModal' + props.keepProp.id).modal('hide')
        } catch (error) {
          logger.error(error)
        }
      }

    }
  },
  components: {}
}
</script>

<style scoped>
.clickable{
cursor: pointer
}
.minview{
  min-height: 200px
}
.txtlft{
    text-align: right;
}
</style>
