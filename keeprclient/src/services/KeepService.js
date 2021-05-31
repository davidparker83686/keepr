import { AppState } from '../AppState'
// import { logger } from '../utils/Logger'
import { api } from './AxiosService'

class KeepsService {
  async getAllKeeps() {
    const res = await api.get('api/keeps')
    AppState.keeps = res.data
  }

  async createKeep(newKeep) {
    newKeep.location = AppState.account.location
    const res = await api.post('api/keeps', newKeep)
    AppState.keeps.push(res.data)
    // this.getKeepsByUserId()
  }

  async getKeepsByUserId(id) {
    const res = await api.get(`api/profile/${id}/keeps`)
    AppState.keeps = res.data
  }

  async deleteKeep(id) {
    await api.delete(`api/keeps/${id}`)
    AppState.keeps = AppState.keeps.filter(i => i.id !== id)
    this.getAll()
  }
}

export const keepsService = new KeepsService()
