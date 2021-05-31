<template>
  <div>
    <div class="modal "
         id="keepDetailsModal"
         tabindex="-1"
         role="dialog"
         aria-labelledby="exampleModalLabel"
         aria-hidden="true"
    >
      <div class="modal-dialog" role="document">
        <div class="modal-content row">
          <div class="col-6"></div>

          <div class="col-6">
            <div class="row">
              <button type="button" class="btn btn-none text-danger" title="close" aria="close" data-dismiss="modal">
                <i class="fas fa-times"></i>
              </button>
            </div>

            <div class="row justify-content-center">
              {{ keep.name }}
            </div>
            <div class="row justify-content-center">
              {{ keep.description }}
            </div>
            <div class="row justify-content-center">
              {{ keep.views }}<i class="far fa-eye"></i>
              {{ keep.keeps }} <i class="fab fa-kaggle"></i>
              {{ keep.shares }}<i class="fas fa-share"></i>
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
                    Add To Vault
                  </button>
                  <div class="dropdown-menu clickable" aria-labelledby="dropdownMenuButton">
                    <div class="dropdown-item clickable" href="#" v-for="vualt in state.vualt" :key="vualt.id" @click="addToVault(vualt.id)">
                      {{ vualt.title }}
                    </div>
                  </div>
                </div>
              </div>

              <div class="col-5 text-right clickable">
                <div v-if="keep.creatorId != state.account.id">
                  <router-link style="color: inherit;" :to="{name: 'Profile', params: {id: keep.creatorId}}">
                    {{ keep.creator.picture }}
                    {{ keep.creator.name }}
                  </router-link>
                </div>
                <div v-if="keep.creatorId == state.account.id">
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
</template>

<script>
import { reactive, computed } from 'vue'
import { keepService } from '../services/KeepService'
import $ from 'jquery'
import { AppState } from '../AppState'
// import { accountService } from '../services/AccountService'
import { logger } from '../utils/Logger'
export default {
  name: 'KeepDetailsModal',
  props: {
    keepDetailsModal: {
      type: Object,
      required: true
    }
  },
  setup() {
    const state = reactive({
      keep: computed(() => AppState.keeps),
      account: computed(() => AppState.account),
      user: computed(() => AppState.user)
    })
    return {
      state,
      async deleteKeep(id) {
        try {
          if (await Notification.confirmAction('Are you sure you want to delete this keep?', 'You won\'t be able to revert this.', '', 'Yes, Delete')) {
            await keepService.deleteKeep(id)
            Notification.toast('Successfully Deleted Keep', 'success')
          }
        } catch (error) {
          logger.error(error)
        }
      },
      async addToVault() {
        try {
          await keepService.addToVault(state.newKeep)
          state.newKeep = {}
          $('#keepDetailsModal').modal('hide')
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
</style>
